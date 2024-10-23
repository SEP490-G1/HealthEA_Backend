using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
namespace Infrastructure.MediatR.Events.Commands.CreateEvent;
public class CreateEventCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public Guid StatusId { get; set; }
    public TimeSpan? ReminderOffset { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly SqlDBContext _context;

    public CreateEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = new Event
        {
            EventId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            EventDateTime = request.EventDateTime.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            StatusId = request.StatusId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.UserId
        };

        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        var reminderOffset = request.ReminderOffset ?? TimeSpan.FromMinutes(30);

        var reminderTime = request.EventDateTime.Add(request.StartTime).Subtract(reminderOffset);

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            EventId = eventEntity.EventId,
            ReminderTime = reminderTime,
            Message = $"Reminder for event: {eventEntity.Title}",
            IsSent = false
        };

        await _context.Reminders.AddAsync(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }
}
