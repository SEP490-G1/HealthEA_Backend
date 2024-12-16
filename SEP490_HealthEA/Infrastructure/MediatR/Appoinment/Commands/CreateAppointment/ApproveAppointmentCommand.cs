using Domain.Common.Exceptions;
using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.Services;
using Infrastructure.Services.Background;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class ApproveAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }
}
public class ApproveAppointmentHandler : IRequestHandler<ApproveAppointmentCommand, bool>
{
    private readonly SqlDBContext _context;
	private readonly IBackgroundTaskQueue queue;

	public ApproveAppointmentHandler(SqlDBContext context, IBackgroundTaskQueue queue)
	{
		_context = context;
		this.queue = queue;
	}


	public async Task<bool> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, cancellationToken);

        if (appointment == null)
        {
            throw new Exception(ErrorCode.APPOINTMENT_NOT_FOUND);
        }
        if (appointment.Status != "Pending")
        {
            throw new Exception("Bác sĩ đã phản hồi lịch hẹn này trước đó.");
        }
        var doctors = await _context.Doctors
            .Include(e => e.User)
            .FirstOrDefaultAsync(d => d.Id == appointment.DoctorId, cancellationToken);
        if (doctors == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }
        var doctor = doctors.User;
        if (doctor == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == appointment.UserId, cancellationToken);

        if (user == null)
        {
            throw new Exception(ErrorCode.USER_NOT_FOUND);
        }

        string location = "N/A";
        if (appointment.Type == "Offline")
        {
            location = doctors.ClinicAddress;
        }
        else if (appointment.Type == "Online")
        {
            location = $"https://meet.example.com/{Guid.NewGuid()}";
        }

        appointment.Status = "Approved";
        appointment.Location = location;
        appointment.UpdatedAt = DateTime.UtcNow;

        var schedule = await _context.Schedules
       .FirstOrDefaultAsync(s =>
           s.DoctorId == appointment.DoctorId &&
           s.Date == appointment.Date &&
           s.StartTime == appointment.StartTime &&
           s.EndTime == appointment.EndTime,
           cancellationToken);
        if (schedule != null)
        {
            schedule.Status = "Unavailable";
            _context.Schedules.Update(schedule);
        }
        //_context.Appointments.Update(appointment);

        var userName = $"{user?.FirstName ?? ""} {user?.LastName ?? ""}".Trim();
        var doctorName = doctors?.DisplayName ?? "Bác sĩ";
        string appointmentDate = appointment?.Date != null ? appointment.Date.ToString("dd/MM/yyyy") : "Chưa xác định";
        string appointmentTime = appointment?.StartTime != null ? appointment.StartTime.ToString(@"hh\:mm") : "Chưa xác định";
        //Queue
        queue.QueueBackgroundWorkItem(async (provider, token) =>
        {
            var emailService = provider.GetRequiredService<EmailService>();
			var context = provider.GetRequiredService<SqlDBContext>();
            var firebase = provider.GetRequiredService<INoticeService>();
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == request.AppointmentId, token);
            if (appointment == null)
            {
                return;
            }
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == appointment.UserId, cancellationToken);
			if (user == null)
			{
				return;
			}
            var doctor = await context.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == appointment.DoctorId, token);
            if (doctor == null)
            {
                return;
            }
            var doctorUser = doctor.User;
			try
            {
                await emailService.SendEmailAsync(
                    user.Email ?? "",
                    "PHẢN HỒI LỊCH KHÁM",
                    $@"
<h2>Xin chào {user.FirstName} {user.LastName},</h2>
<p>Chúng tôi vui mừng thông báo rằng <b>lịch khám</b> của bạn đã được bác sĩ <b>{doctor.DisplayName}</b> chấp nhận.</p>
<p>Dưới đây là thông tin chi tiết về lịch hẹn của bạn:</p>
<ul>
    <li><b>Loại cuộc hẹn:</b> {appointment?.Type ?? "Chưa xác định"}</li>
    <li><b>Ngày:</b> {appointmentDate}</li>
    <li><b>Thời gian:</b> {appointmentTime}</li>
    <li><b>Địa điểm:</b> {location ?? "Chưa xác định"}</li>
</ul>
<p>Vui lòng đến đúng giờ và mang theo các giấy tờ cần thiết nếu có. Nếu bạn cần hỗ trợ thêm, vui lòng liên hệ với chúng tôi.</p>
<p>Trân trọng,</p>
<p><b>G1_SEP490</b></p>"
                );
				//Notice
				Notice userNotice = new Notice()
				{
					CreatedAt = DateTime.Now,
					Message = $"Cuộc hẹn với bác sĩ {doctorUser!.FirstName} {doctorUser.LastName} đã được chấp nhận.",
					HasViewed = false,
					NoticeId = new Guid(),
					RecipientId = user.UserId,
					UserId = doctor.UserId,
				};
                await firebase.CreateAndSendNoticeAsync(userNotice, "Thông báo về lịch hẹn.");

			}
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }

        });


        var eventEntity = new Event
        {
            EventId = new Guid(),
            //UserName = userName,
            Title = $"Cuộc hẹn với bác sĩ {doctorName}",
            Description = appointment.Description,
            EventDateTime = appointment.Date,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime ?? TimeSpan.Zero,
            Location = location,
            //Status = "Approved",
            CreatedAt = DateTime.UtcNow,
            //CreatedBy = doctor?.Email,
            Type = 2
        };
        _context.Events.Add(eventEntity);

        var userEvent = new UserEvent
        {
            UserEventId = Guid.NewGuid(),
            UserId = user.UserId,
            EventId = eventEntity.EventId,

        };

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            EventId = eventEntity.EventId,
            ReminderTime = appointment.Date.AddDays(-1).Add(appointment.StartTime),
            Message = $"Bạn có lịch hẹn với bác sĩ {doctorName} vào lúc {appointmentTime} ngày {appointmentDate}. Địa điểm: {location}.",
        };

        _context.UserEvents.Add(userEvent);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
