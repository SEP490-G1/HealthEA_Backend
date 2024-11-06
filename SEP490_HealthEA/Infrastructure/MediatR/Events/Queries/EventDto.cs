using Domain.Enum;
using Domain.Models.Entities;

namespace Infrastructure.MediatR.Events.Queries;

public class EventDto
{
    public Guid EventId { get; set; }
    public string? UserName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public EventStatusConstants? Status { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; }
    public int RepeatInterval { get; set; }
    public DateTime RepeatEndDate { get; set; }
    public List<UserEventDto> UserEvents { get; set; }
}

