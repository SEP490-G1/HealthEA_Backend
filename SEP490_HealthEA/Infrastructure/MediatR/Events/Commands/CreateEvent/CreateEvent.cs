using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.MediatR.Events.Commands.CreateEvent;
public class CreateEventCommand : IRequest<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public EventStatusConstants Status { get; set; }
    public TimeSpan? ReminderOffset { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
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
            Title = request.Title,
            Description = request.Description,
            EventDateTime = request.EventDateTime.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Status = EventStatusConstants.Pending,
            //ReminderOffset = request.ReminderOffset,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        await _context.Events.AddAsync(eventEntity, cancellationToken);

        // Tạo Reminder dựa trên Event
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

        await _context.Reminders.AddAsync(reminder, cancellationToken);
        foreach (var userId in request.UserIds)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
            if (!userExists)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            var eventUser = new UserEvent
            {
                UserEventId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                UserId = userId
            };

            await _context.UserEvents.AddAsync(eventUser, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }
}
