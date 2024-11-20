using Domain.Common.Exceptions;
using Domain.Models.Entities;
using FluentValidation;
using Google;
using Infrastructure.MediatR.Notices;
using Infrastructure.Services;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public Guid DoctorId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Type { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public string? Status { get; set; } = "Pending";
}

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{

    private readonly SqlDBContext _context;
    private readonly EmailService _emailService;
    private readonly FirebaseNotificationService _firebaseNotificationService;

    public CreateAppointmentHandler(SqlDBContext context, EmailService emailService, FirebaseNotificationService firebaseNotificationService)
    {
        _context = context;
        _emailService = emailService;
        _firebaseNotificationService = firebaseNotificationService;
    }
    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var doctorExists = await _context.Doctors.FirstOrDefaultAsync(d => d.Id.Equals(request.DoctorId), cancellationToken);
        if (doctorExists == null)
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);

        var userExists = await _context.Users.AnyAsync(u => u.UserId == request.UserId, cancellationToken);
        if (!userExists)
            throw new Exception(ErrorCode.USER_NOT_FOUND);
        var schedule = await _context.Schedules
        .FirstOrDefaultAsync(s => s.DoctorId == request.DoctorId
                                  && s.Date == request.Date
                                  && s.StartTime == request.StartTime
                                  && s.Status == "Available", cancellationToken);
        if (schedule == null)
        {
            throw new Exception(ErrorCode.SCHEDULE_NOT_FOUND);
        }
        string title = $"Tư vấn về {request.Title} với {doctorExists.DisplayName}";
        string address = doctorExists.ClinicAddress;
        string location = "test";

        var appointment = new Appointment
        {
            DoctorId = request.DoctorId,
            UserId = request.UserId,
            Title = title,
            Description = request.Description,
            Date = request.Date,
            StartTime = request.StartTime,
            EndTime = schedule.EndTime,
            Type = request.Type,
            //Location = location,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);

        var schedules = await _context.Schedules
      .FirstOrDefaultAsync(s =>
          s.DoctorId == appointment.DoctorId &&
          s.Date == appointment.Date &&
          s.StartTime == appointment.StartTime &&
          s.EndTime == appointment.EndTime,
          cancellationToken);
        if (schedule != null)
        {
            schedule.Status = "Unavailable";
            _context.Schedules.Update(schedules);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == appointment.UserId, cancellationToken);
        var doctor = await _context.Users.Include(u => u.Doctor).FirstOrDefaultAsync(u => u.Doctor!.Id == appointment.DoctorId, cancellationToken);

        //var doctorDeviceToken = await _context.DeviceTokens
        //   .Where(dt => dt.UserId == doctor.UserId)
        //   .Select(dt => dt.DeviceToken)
        //   .FirstOrDefaultAsync();

        //var userDeviceToken = await _context.DeviceTokens
        //    .Where(dt => dt.UserId == user.UserId)
        //    .Select(dt => dt.DeviceToken)
        //    .FirstOrDefaultAsync();

        //if (!string.IsNullOrEmpty(doctorDeviceToken))
        //{
        //    await _firebaseNotificationService.SendNotificationAsync(
        //        doctorDeviceToken,
        //        "Cuộc hẹn mới",
        //        $"Bạn có một cuộc hẹn mới với bệnh nhân {user.FirstName} {user.LastName}."
        //    );
        //}

        //if (!string.IsNullOrEmpty(userDeviceToken))
        //{
        //    await _firebaseNotificationService.SendNotificationAsync(
        //        userDeviceToken,
        //        "Cuộc hẹn của bạn đã được tạo",
        //        $"Cuộc hẹn với bác sĩ {doctor.FirstName} {doctor.LastName} đã được xác nhận."
        //    );
        //}
        Thread firebaseThread = new Thread(() =>
        {
            try
            {
                 SendFirebaseNotificationsAsync(user, doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending emails: {ex.Message}");
            }
        });

        Thread emailThread = new Thread(() =>
        {
            try
            {
                if (user != null && doctor != null)
                {
                    _emailService.SendAppointmentEmailsAsync(
                        userEmail: user.Email,
                        doctorEmail: doctor.Email,
                        userName: $"{user.FirstName} {user.LastName}",
                        doctorName: $"{doctor.FirstName} {doctor.LastName}"
                    ).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending emails: {ex.Message}");
            }
        });

        emailThread.Start();
        Thread.Sleep(100);
        firebaseThread.Start();
        Thread.Sleep(100);
        //emailThread.Join();
        //firebaseThread.Join();
        await _context.SaveChangesAsync(cancellationToken);

        return appointment.AppointmentId;
    }
    private async Task SendFirebaseNotificationsAsync(User user, User doctor)
    {
        var doctorDeviceToken = await _context.DeviceTokens
            .Where(dt => dt.UserId == doctor.UserId)
            .Select(dt => dt.DeviceToken)
            .FirstOrDefaultAsync();

        var userDeviceToken = await _context.DeviceTokens
            .Where(dt => dt.UserId == user.UserId)
            .Select(dt => dt.DeviceToken)
            .FirstOrDefaultAsync();

        if (!string.IsNullOrEmpty(doctorDeviceToken))
        {
            await _firebaseNotificationService.SendNotificationAsync(
                doctorDeviceToken,
                "Cuộc hẹn mới",
                $"Bạn có một cuộc hẹn mới với bệnh nhân {user.FirstName} {user.LastName}."
            );
        }

        if (!string.IsNullOrEmpty(userDeviceToken))
        {
            await _firebaseNotificationService.SendNotificationAsync(
                userDeviceToken,
                "Cuộc hẹn của bạn đã được tạo",
                $"Cuộc hẹn với bác sĩ {doctor.FirstName} {doctor.LastName} đã được xác nhận."
            );
        }
    }

}