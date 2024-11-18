using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateAllEventCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public Guid? OriginalEventId { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? EventDateTime { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
    public EventDailyConstants? RepeatFrequency { get; set; }
    public int? RepeatInterval { get; set; }
    public DateTime? RepeatEndDate { get; set; }
    public List<ReminderOffsetDto> ReminderOffsets { get; set; } = new List<ReminderOffsetDto>();
}

public class UpdateAllEventCommandHandler : IRequestHandler<UpdateAllEventCommand, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateAllEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateAllEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntities = await _context.Events
            .Include(e => e.Reminders)
            .Where(e => e.EventId == request.EventId || e.OriginalEventId == request.OriginalEventId)
            .ToListAsync(cancellationToken);

        if (!eventEntities.Any())
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        var userEvents = await _context.UserEvents
       .Where(ue => ue.EventId == request.EventId && ue.UserId == request.UserId)
       .ToListAsync(cancellationToken);

        if (!userEvents.Any())
        {
            throw new Exception(ErrorCode.UNAUTHORIZED_ACCESS);
        }
        var originalEvent = eventEntities.FirstOrDefault(e => e.EventId == request.EventId) ?? eventEntities.First();

        originalEvent.Title = request.Title ?? originalEvent.Title;
        originalEvent.Description = request.Description ?? originalEvent.Description;
        originalEvent.EventDateTime = request.EventDateTime?.Date ?? originalEvent.EventDateTime;
        originalEvent.StartTime = request.StartTime ?? originalEvent.StartTime;
        originalEvent.EndTime = request.EndTime ?? originalEvent.EndTime;
        originalEvent.Location = request.Location ?? originalEvent.Location;
        originalEvent.UpdatedAt = DateTime.UtcNow;
        originalEvent.RepeatFrequency = request.RepeatFrequency ?? originalEvent.RepeatFrequency;
        originalEvent.RepeatInterval = request.RepeatInterval ?? originalEvent.RepeatInterval;
        originalEvent.RepeatEndDate = request.RepeatEndDate ?? originalEvent.RepeatEndDate;
        var eventIdsToDelete = eventEntities.Select(e => e.EventId).ToList();
        var userEventsToDelete = await _context.UserEvents
       .Where(ue => eventIdsToDelete.Contains(ue.EventId))
       .ToListAsync(cancellationToken);

        _context.Reminders.RemoveRange(eventEntities.SelectMany(e => e.Reminders));
        _context.UserEvents.RemoveRange(userEventsToDelete);
        _context.Events.RemoveRange(eventEntities.Where(e => e.OriginalEventId == request.OriginalEventId));

        DateTime reminderDateTime = originalEvent.EventDateTime.Date.Add(originalEvent.StartTime);
        int interval = originalEvent.RepeatInterval > 0 ? originalEvent.RepeatInterval : 1;

        while (reminderDateTime <= originalEvent.RepeatEndDate)
        {
            var eventClone = new Event
            {
                EventId = Guid.NewGuid(),
                OriginalEventId = originalEvent.OriginalEventId,
                Title = originalEvent.Title,
                Description = originalEvent.Description,
                EventDateTime = reminderDateTime,
                StartTime = originalEvent.StartTime,
                EndTime = originalEvent.EndTime,
                Location = originalEvent.Location,
                RepeatFrequency = originalEvent.RepeatFrequency,
                RepeatInterval = originalEvent.RepeatInterval,
                RepeatEndDate = originalEvent.RepeatEndDate,
                CreatedAt = originalEvent.CreatedAt,
                CreatedBy = originalEvent.CreatedBy
            };

            await _context.Events.AddAsync(eventClone, cancellationToken);

            var userEvent = new UserEvent
            {
                UserEventId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = eventClone.EventId,
                IsAccepted = true,
                IsOrganizer = true
            };

            await _context.UserEvents.AddAsync(userEvent, cancellationToken);

            foreach (var reminderOffsetDto in request.ReminderOffsets)
            {
                var reminderTime = CalculateReminderTime(reminderDateTime, reminderOffsetDto);

                var reminder = new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventClone.EventId,
                    ReminderOffset = reminderOffsetDto.OffsetValue,
                    OffsetUnit = reminderOffsetDto.OffsetUnit,
                    ReminderTime = reminderTime,
                    Message = $"Reminder for event: {eventClone.Title}",
                    IsSent = false
                };

                await _context.Reminders.AddAsync(reminder, cancellationToken);
            }

            switch (originalEvent.RepeatFrequency)
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
                    reminderDateTime = originalEvent.RepeatEndDate.AddDays(1); 
                    break;
                default:
                    break;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return originalEvent.OriginalEventId ?? originalEvent.EventId;
    }


    private DateTime CalculateReminderTime(DateTime eventDateTime, ReminderOffsetDto reminderOffsetDto)
    {
        return reminderOffsetDto.OffsetUnit switch
        {
            OffsetUnitContants.minutes => eventDateTime.AddMinutes(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.hours => eventDateTime.AddHours(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.days => eventDateTime.AddDays(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.weeks => eventDateTime.AddDays(-reminderOffsetDto.OffsetValue * 7),
            _ => eventDateTime
        };
    }
}
