using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.Services;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class ApproveAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; set; }
}
public class ApproveAppointmentHandler : IRequestHandler<ApproveAppointmentCommand, bool>
{
    private readonly SqlDBContext _context;
    private readonly EmailService _emailService;

    public ApproveAppointmentHandler(SqlDBContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
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
            .FirstOrDefaultAsync(d => d.Id == appointment.DoctorId, cancellationToken);
        if (doctors == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }
        var doctor = await _context.Users
            .FirstOrDefaultAsync(d => d.UserId == appointment.DoctorId, cancellationToken);
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

        var userName = $"{user?.FirstName ?? ""} {user?.LastName ?? ""}".Trim();
        var doctorName = doctors?.DisplayName ?? "Bác sĩ";
        string appointmentDate = appointment?.Date != null ? appointment.Date.ToString("dd/MM/yyyy") : "Chưa xác định";
        string appointmentTime = appointment?.StartTime != null ? appointment.StartTime.ToString(@"hh\:mm") : "Chưa xác định";

        _emailService.SendEmail(
            user?.Email ?? "",
            "PHẢN HỒI LỊCH KHÁM",
            $@"
    <h2>Xin chào {userName},</h2>
    <p>Chúng tôi vui mừng thông báo rằng <b>lịch khám</b> của bạn đã được bác sĩ <b>{doctorName}</b> chấp nhận.</p>
    <p>Dưới đây là thông tin chi tiết về lịch hẹn của bạn:</p>
    <ul>
        <li><b>Loại cuộc hẹn:</b> {appointment?.Type ?? "Chưa xác định"}</li>
        <li><b>Ngày:</b> {appointmentDate}</li>
        <li><b>Thời gian:</b> {appointmentTime}</li>
        <li><b>Địa điểm:</b> {location ?? "Chưa xác định"}</li>
    </ul>
    <p>Vui lòng đến đúng giờ và mang theo các giấy tờ cần thiết nếu có. Nếu bạn cần hỗ trợ thêm, vui lòng liên hệ với chúng tôi.</p>
    <p>Trân trọng,</p>
    <p><b>G1_SEP490</b></p>
    "
        );

        var eventEntity = new Event
        {
            EventId = appointment.EventId,
            UserName = userName,
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
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
