using Domain.Enum;
using System;

namespace Domain.Models.Entities;
public class Event
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public Guid? OriginalEventId { get; set; } = Guid.NewGuid();
    public string? UserName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }  // Thời điểm diễn ra sự kiện
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public int Type { get; set; } = 1;
    public EventStatusConstants? Status { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; } // Tần suất lặp lại
    public int RepeatInterval { get; set; } = 1; // Khoảng thời gian lặp lại
    public DateTime RepeatEndDate { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public virtual ICollection<Reminder> Reminders { get; set; }
    public virtual ICollection<UserEvent> UserEvents { get; set; }

}

