using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.CreateEvent;

public class CreateEventWithUserCommand : IRequest<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public EventStatusConstants Status { get; set; } = EventStatusConstants.Pending; // Default to Pending
    public EventDailyConstants RepeatFrequency { get; set; } = EventDailyConstants.NotRepeat; // Default to Monthly
    public int RepeatInterval { get; set; } = 1;
    public DateTime RepeatEndDate { get; set; }
    public TimeSpan? ReminderOffset { get; set; } = TimeSpan.FromMinutes(30); // Default reminder offset
    public List<Guid> UserIds { get; set; } = new();
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}

public class CreateEventWithUserCommandHandler : IRequestHandler<CreateEventWithUserCommand, Guid>
{
    private readonly SqlDBContext _context;

    public CreateEventWithUserCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateEventWithUserCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Create the event entity from the request
        var eventEntity = new Event
        {
            Title = request.Title,
            Description = request.Description,
            EventDateTime = request.EventDateTime.Date,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Location = request.Location,
            Status = request.Status,
            RepeatFrequency = request.RepeatFrequency,
            RepeatInterval = request.RepeatInterval,
            RepeatEndDate = request.RepeatEndDate,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        await _context.Events.AddAsync(eventEntity, cancellationToken);

        // Generate reminders based on RepeatFrequency
        DateTime reminderDateTime = request.EventDateTime.Date.Add(request.StartTime);
        int interval = request.RepeatInterval > 0 ? request.RepeatInterval : 1;

        while (reminderDateTime <= request.RepeatEndDate)
        {
            // Calculate the reminder time based on offset
            var reminderOffset = request.ReminderOffset ?? TimeSpan.FromMinutes(30);
            var reminderTime = reminderDateTime.Subtract(reminderOffset);

            // Determine offset unit and value
            OffsetUnitContants offsetUnit = DetermineOffsetUnit(reminderOffset);
            int offsetValue = offsetUnit switch
            {
                OffsetUnitContants.minutes => (int)reminderOffset.TotalMinutes,
                OffsetUnitContants.hours => (int)reminderOffset.TotalHours,
                OffsetUnitContants.days => (int)reminderOffset.TotalDays,
                _ => 0
            };

            // Add reminder to context
            var reminder = new Reminder
            {
                ReminderId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                ReminderOffset = offsetValue,
                OffsetUnit = offsetUnit,
                ReminderTime = reminderTime,
                Message = $"Reminder for event: {eventEntity.Title}",
                IsSent = false
            };

            await _context.Reminders.AddAsync(reminder, cancellationToken);

            // Move reminderDateTime to the next occurrence based on RepeatFrequency
            switch (request.RepeatFrequency)
            {
                case EventDailyConstants.Daily:
                    reminderDateTime = reminderDateTime.AddDays(interval);
                    break;

                case EventDailyConstants.Weekly:
                    reminderDateTime = reminderDateTime.AddDays(7 * interval);
                    break;

                case EventDailyConstants.Monthly:
                    reminderDateTime = reminderDateTime.AddMonths(interval);
                    break;

                case EventDailyConstants.Yearly:
                    reminderDateTime = reminderDateTime.AddYears(interval);
                    break;
                case EventDailyConstants.NotRepeat:
                    reminderDateTime = request.RepeatEndDate.AddDays(1);
                    break;
                default:
                    break;
            }

        }

        // Link users to the event
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

        // Save all changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }


    // Phương thức xác định OffsetUnit từ ReminderOffset
    private OffsetUnitContants DetermineOffsetUnit(TimeSpan offset)
    {
        if (offset.TotalMinutes < 60)
            return OffsetUnitContants.minutes;
        if (offset.TotalHours < 24)
            return OffsetUnitContants.hours;
        return OffsetUnitContants.days;
    }
}
