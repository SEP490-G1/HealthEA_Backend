using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.Services;
using Infrastructure.Services.Background;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class RejectAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }
}

public class RejectAppointmentHandler : IRequestHandler<RejectAppointmentCommand, bool>
{
    private readonly SqlDBContext _context;
	private readonly IBackgroundTaskQueue queue;
	public RejectAppointmentHandler(SqlDBContext context, IBackgroundTaskQueue queue)
	{
		_context = context;
		this.queue = queue;
	}

	public async Task<bool> Handle(RejectAppointmentCommand request, CancellationToken cancellationToken)
    {
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
        appointment.Status = "Rejected";
        appointment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var userName = $"{user?.FirstName ?? ""} {user?.LastName ?? ""}".Trim();
        var doctorName = doctors?.DisplayName ?? "Bác sĩ";

		queue.QueueBackgroundWorkItem(async (provider, token) =>
		{
			var emailService = provider.GetRequiredService<EmailService>();
			var context = provider.GetRequiredService<SqlDBContext>();
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
			try
			{
				await emailService.SendEmailAsync(
	user?.Email ?? "",
	"PHẢN HỒI LỊCH KHÁM",
	$@"
    <h2>Xin chào {user?.FirstName} {user?.LastName},</h2>
    <p>Chúng tôi rất tiếc phải thông báo rằng bác sĩ đã từ chối lịch hẹn khám của bạn.</p>
    <p>Vui lòng liên hệ lại nếu bạn cần hỗ trợ hoặc đặt lịch hẹn khác.</p>
    <p>Trân trọng,</p>
    <p><b>G1_SEP490</b></p>
    "
);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to send email: {ex.Message}");
			}

		});

        return true;
    }

   
}
