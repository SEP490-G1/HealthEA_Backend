using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent
{
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

            // Fetch all events matching either the EventId or the OriginalEventId
            var eventEntities = await _context.Events
                .Include(e => e.Reminders)
                .Where(e => e.EventId == request.EventId || e.OriginalEventId == request.OriginalEventId)
                .ToListAsync(cancellationToken);

            if (!eventEntities.Any())
                throw new Exception(ErrorCode.EVENT_NOT_FOUND);

            // Check user access
            var userEvents = await _context.UserEvents
                .Where(ue => ue.EventId == request.EventId && ue.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            if (!userEvents.Any())
                throw new Exception(ErrorCode.UNAUTHORIZED_ACCESS);

            var originalEvent = eventEntities.FirstOrDefault(e => e.EventId == request.EventId) ?? eventEntities.First();

            // Update event details
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

            // Remove old events, user-events and reminders
            var eventIdsToDelete = eventEntities.Select(e => e.EventId).ToList();
            var userEventsToDelete = await _context.UserEvents
                .Where(ue => eventIdsToDelete.Contains(ue.EventId))
                .ToListAsync(cancellationToken);

            _context.Reminders.RemoveRange(eventEntities.SelectMany(e => e.Reminders));
            _context.UserEvents.RemoveRange(userEventsToDelete);
            _context.Events.RemoveRange(eventEntities.Where(e => e.OriginalEventId == request.OriginalEventId));

            // Check for time validity
            DateTime reminderDateTime = originalEvent.EventDateTime.Date.Add(originalEvent.StartTime);
            DateTime reminderEndDateTime = originalEvent.RepeatEndDate.Date.Add(originalEvent.EndTime);
            int interval = originalEvent.RepeatInterval > 0 ? originalEvent.RepeatInterval : 1;

            if (reminderDateTime > originalEvent.RepeatEndDate)
                throw new Exception("RemindDateTime phải nhỏ hơn RepeatEndDate");

            if (originalEvent.StartTime > originalEvent.EndTime)
                throw new Exception("StartTime phải nhỏ hơn EndTime");

            // Generate new events based on repeat frequency
            while (reminderDateTime <= reminderEndDateTime)
            {
                // Check if an event with the same time already exists
                var existingEvent = await _context.Events
                    .Where(e => e.EventDateTime == reminderDateTime && e.StartTime == request.StartTime && e.EndTime == request.EndTime)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingEvent != null)
                {
                    // If event exists, skip to the next time slot
                    reminderDateTime = GetNextEventDateTime(reminderDateTime, request.RepeatFrequency, interval);
                    continue;
                }

                // Create new event clone
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

                // Create user event link
                var userEvent = new UserEvent
                {
                    UserEventId = Guid.NewGuid(),
                    UserId = request.UserId,
                    EventId = eventClone.EventId,
                    IsAccepted = true,
                    IsOrganizer = true
                };

                await _context.UserEvents.AddAsync(userEvent, cancellationToken);

                // Create reminders for the new event
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

                // Update the reminder date time for the next iteration
                reminderDateTime = GetNextEventDateTime(reminderDateTime, originalEvent.RepeatFrequency, originalEvent.RepeatInterval);
            }

            // Save changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return originalEvent.OriginalEventId;
        }

        // Calculate next event time based on frequency
        private DateTime GetNextEventDateTime(DateTime currentDateTime, EventDailyConstants? repeatFrequency, int repeatInterval)
        {
            return repeatFrequency switch
            {
                EventDailyConstants.Daily => currentDateTime.AddDays(repeatInterval),
                EventDailyConstants.Weekly => currentDateTime.AddDays(7 * repeatInterval),
                EventDailyConstants.Monthly => currentDateTime.AddMonths(repeatInterval),
                EventDailyConstants.Yearly => currentDateTime.AddYears(repeatInterval),
                _ => currentDateTime // No repeat or invalid repeat frequency
            };
        }

        // Calculate reminder time based on offset
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
}
