using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;

namespace Infrastructure.MediatR.Events.Commands.CreateEvent;

public class CreateEventCommand : IRequest<Guid>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; } = EventDailyConstants.NotRepeat;
    public int RepeatInterval { get; set; } = 1;
    public DateTime RepeatEndDate { get; set; }
    public List<ReminderOffsetDto> ReminderOffsets { get; set; } = new List<ReminderOffsetDto>();
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
            RepeatFrequency = request.RepeatFrequency,
            RepeatInterval = request.RepeatInterval,
            RepeatEndDate = request.RepeatEndDate,
            CreatedAt = request.CreatedAt ?? DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        await _context.Events.AddAsync(eventEntity, cancellationToken);

        DateTime reminderDateTime = request.EventDateTime.Date.Add(request.StartTime);
        int interval = request.RepeatInterval > 0 ? request.RepeatInterval : 1;

        // Tạo reminders từ ngày bắt đầu đến ngày kết thúc theo RepeatFrequency
        while (reminderDateTime <= request.RepeatEndDate)
        {
            foreach (var reminderOffsetDto in request.ReminderOffsets)
            {
                var reminderTime = CalculateReminderTime(reminderDateTime, reminderOffsetDto);

                // Add reminder to context
                var reminder = new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventEntity.EventId,
                    ReminderOffset = reminderOffsetDto.OffsetValue,
                    OffsetUnit = reminderOffsetDto.OffsetUnit,
                    ReminderTime = reminderTime,
                    Message = $"Reminder for event: {eventEntity.Title}",
                    IsSent = false
                };

                await _context.Reminders.AddAsync(reminder, cancellationToken);
            }

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
                    reminderDateTime = request.RepeatEndDate.AddDays(1); // Exit loop
                    break;
                default:
                    break;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
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

