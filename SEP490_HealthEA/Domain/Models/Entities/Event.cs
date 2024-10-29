using Domain.Enum;
using System;

namespace Domain.Models.Entities;
public class Event
{
    public Guid EventId { get; set; } = Guid.Empty;
    public string? UserName { get; set; }  
    public string? Title { get; set; }  
    public string? Description { get; set; } 
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; } 
    public EventStatusConstants Status { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; } //tan suat lap lai
    public int RepeatInterval { get; set; } = 1; //khoag thoi gian lap lai
    public DateTime RepeatEndDate { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; } 
    public DateTime? UpdatedAt { get; set; }  
    public string? UpdatedBy { get; set; }
    public virtual ICollection<Reminder> Reminders { get; set; }
    public virtual ICollection<UserEvent> UserEvents { get; set; }

}

