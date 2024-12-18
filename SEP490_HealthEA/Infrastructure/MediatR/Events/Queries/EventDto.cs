﻿using Domain.Enum;
using Domain.Models.Entities;

namespace Infrastructure.MediatR.Events.Queries;

public class EventDto
{
    public Guid EventId { get; set; }
    public Guid OriginalEventId { get; set; }
    //public string? UserName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public int Type { get; set; }
    public EventStatusConstants? Status { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; } = EventDailyConstants.NotRepeat;
    //public int RepeatInterval { get; set; }
    public DateTime RepeatEndDate { get; set; }
    //public List<UserEventDto> UserEvents { get; set; }
    public List<ReminderOffsetDto> ReminderOffsetDtos { get; set; }
}

