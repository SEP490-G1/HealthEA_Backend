﻿using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

        DateTime reminderDateTime = request.EventDateTime.Date.Add(request.StartTime); 
        DateTime reminderEndDateTime = request.RepeatEndDate.Date.Add(request.EndTime); 
        int interval = request.RepeatInterval > 0 ? request.RepeatInterval : 1; 
        Guid originalEventId = Guid.NewGuid(); 

        if (reminderDateTime > reminderEndDateTime)
        {
            throw new Exception("RemindDateTime phải nhỏ hơn RepeatEndDate");
        }

        //if (request.StartTime > request.EndTime)
        //{
        //    throw new Exception("StartTime phải nhỏ hơn EndTime");
        //}

        while (reminderDateTime <= reminderEndDateTime)
        {
            var existingEvent = await _context.Events
                .Where(e => e.EventDateTime == reminderDateTime && e.StartTime == request.StartTime && e.EndTime == request.EndTime)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingEvent != null)
            {
                reminderDateTime = request.RepeatFrequency switch
                {
                    EventDailyConstants.Daily => reminderDateTime.AddDays(interval),
                    EventDailyConstants.Weekly => reminderDateTime.AddDays(7 * interval),
                    EventDailyConstants.Monthly => reminderDateTime.AddMonths(interval),
                    EventDailyConstants.Yearly => reminderDateTime.AddYears(interval),
                    _ => request.RepeatEndDate.AddDays(1) 
                };
                continue;
            }

            var eventEntity = new Event
            {
                EventId = Guid.NewGuid(),
                OriginalEventId = originalEventId,
                Title = request.Title,
                Description = request.Description,
                EventDateTime = reminderDateTime,
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

            var userEvent = new UserEvent
            {
                UserEventId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = eventEntity.EventId,
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
                    EventId = eventEntity.EventId,
                    ReminderOffset = reminderOffsetDto.OffsetValue,
                    OffsetUnit = reminderOffsetDto.OffsetUnit,
                    ReminderTime = reminderTime,
                    Message = $"Reminder for event: {eventEntity.Title}",
                    IsSent = false
                };

                await _context.Reminders.AddAsync(reminder, cancellationToken);
            }

            reminderDateTime = request.RepeatFrequency switch
            {
                EventDailyConstants.Daily => reminderDateTime.AddDays(interval),
                EventDailyConstants.Weekly => reminderDateTime.AddDays(7 * interval),
                EventDailyConstants.Monthly => reminderDateTime.AddMonths(interval),
                EventDailyConstants.Yearly => reminderDateTime.AddYears(interval),
                _ => request.RepeatEndDate.AddDays(1) 
            };
        }

        await _context.SaveChangesAsync(cancellationToken);

        return originalEventId;
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
