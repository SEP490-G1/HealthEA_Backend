using Domain.Models.Entities;
using Google;
using Infrastructure.SQLServer;
using MediatR;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public string Status { get; set; } = "Pending";
}

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly SqlDBContext _context;

    public CreateAppointmentHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            AppointmentId = Guid.NewGuid(),
            EventId = request.EventId,
            DoctorId = request.DoctorId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync(cancellationToken);

        return appointment.AppointmentId;
    }
}