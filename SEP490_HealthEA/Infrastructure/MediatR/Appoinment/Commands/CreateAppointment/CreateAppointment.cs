using Domain.Common.Exceptions;
using Domain.Models.Entities;
using FluentValidation;
using Google;
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

    public CreateAppointmentHandler(SqlDBContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
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


        await _context.SaveChangesAsync(cancellationToken);

        return appointment.AppointmentId;
    }
}