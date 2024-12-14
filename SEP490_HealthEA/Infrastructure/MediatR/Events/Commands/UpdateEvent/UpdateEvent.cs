using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? EventDateTime { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
    public List<ReminderOffsetDto>? ReminderOffsets { get; set; }
    public DateTime? RepeatEndDate { get; set; }
    public int RepeatInterval { get; set; } = 1; // Default repeat interval is 1 day
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Fetch the event
        var eventEntity = await _context.Events
            .Include(e => e.Reminders)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception("Event not found");
        }

        // Validate user access
        var userEvent = await _context.UserEvents
            .FirstOrDefaultAsync(ue => ue.EventId == request.EventId && ue.UserId == request.UserId, cancellationToken);

        if (userEvent == null)
        {
            throw new UnauthorizedAccessException("User does not have access to this event");
        }

        // Validate StartTime and EndTime
        if (request.StartTime.HasValue && request.EndTime.HasValue && request.StartTime >= request.EndTime)
        {
            throw new Exception("StartTime must be earlier than EndTime");
        }

        // Update event details
        eventEntity.Title = request.Title ?? eventEntity.Title;
        eventEntity.Description = request.Description ?? eventEntity.Description;
        eventEntity.EventDateTime = request.EventDateTime ?? eventEntity.EventDateTime;
        eventEntity.StartTime = request.StartTime ?? eventEntity.StartTime;
        eventEntity.EndTime = request.EndTime ?? eventEntity.EndTime;
        eventEntity.Location = request.Location ?? eventEntity.Location;

        // Manage reminders
        _context.Reminders.RemoveRange(eventEntity.Reminders); // Clear existing reminders

        if (request.ReminderOffsets != null && request.ReminderOffsets.Any())
        {
            var eventStartTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime);
            var newReminders = request.ReminderOffsets.Select(r =>
            {
                var reminderTime = r.OffsetUnit switch
                {
                    OffsetUnitContants.minutes => eventStartTime.AddMinutes(-r.OffsetValue),
                    OffsetUnitContants.hours => eventStartTime.AddHours(-r.OffsetValue),
                    OffsetUnitContants.days => eventStartTime.AddDays(-r.OffsetValue),
                    _ => throw new Exception("Invalid offset unit")
                };

                return new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventEntity.EventId,
                    OffsetUnit = r.OffsetUnit,
                    ReminderTime = reminderTime,
                    ReminderOffset = r.OffsetValue,
                    IsSent = false,
                    Message = $"Reminder for event: {eventEntity.Title}"
                };
            }).ToList();

            await _context.Reminders.AddRangeAsync(newReminders, cancellationToken);
        }
        else if (request.RepeatEndDate.HasValue)
        {
            var reminderDateTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime);
            var reminderEndDateTime = request.RepeatEndDate.Value.Date.Add(eventEntity.EndTime);
            var interval = Math.Max(request.RepeatInterval, 1); // Ensure interval is valid

            if (reminderDateTime > reminderEndDateTime)
            {
                throw new Exception("Reminder start time must be earlier than the repeat end date");
            }

            var reminders = new List<Reminder>();
            while (reminderDateTime <= reminderEndDateTime)
            {
                reminders.Add(new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventEntity.EventId,
                    ReminderTime = reminderDateTime,
                    IsSent = false,
                    Message = $"Reminder for event: {eventEntity.Title}"
                });

                reminderDateTime = reminderDateTime.AddDays(interval);
            }

            await _context.Reminders.AddRangeAsync(reminders, cancellationToken);
        }
        else
        {
            var defaultReminder = new Reminder
            {
                ReminderId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                ReminderTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime),
                IsSent = false,
                Message = $"Reminder for event: {eventEntity.Title}"
            };

            await _context.Reminders.AddAsync(defaultReminder, cancellationToken);
        }

        // Save changes
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }
}
