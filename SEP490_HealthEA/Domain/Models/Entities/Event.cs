using Domain.Enum;
using MediatR;
using System;

namespace Domain.Models.Entities;

/// <summary>
/// Represents an event with details such as title, description, schedule, location, and recurrence settings.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// Defaults to a new Guid.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the identifier of the original event if this event is a duplicate or related to another.
    /// Defaults to a new Guid.
    /// </summary>
    public Guid OriginalEventId { get; set; } = Guid.NewGuid();
    //public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event occurs.
    /// </summary>
    public DateTime EventDateTime { get; set; }  // Thời điểm diễn ra sự kiện

    /// <summary>
    /// Gets or sets the start time of the event.
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the event.
    /// </summary>
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Gets or sets the location of the event.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the type of the event, represented as an integer.
    /// Defaults to 1.
    /// </summary>
    public int Type { get; set; } = 1;

    /// <summary>
    /// Gets or sets the status of the event.
    /// </summary>
    public EventStatusConstants? Status { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the event's recurrence.
    /// </summary>
    public EventDailyConstants RepeatFrequency { get; set; } // Tần suất lặp lại

    /// <summary>
    /// Gets or sets the interval for the event's recurrence.
    /// Defaults to 1.
    /// </summary>
    public int RepeatInterval { get; set; } = 1; // Khoảng thời gian lặp lại

    /// <summary>
    /// Gets or sets the date when the recurrence of the event ends.
    /// </summary>
    public DateTime RepeatEndDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the event.
    /// Defaults to the current UTC time.
    /// </summary>
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the user who created the event.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp of the event.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who last updated the event.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the collection of reminders associated with the event.
    /// </summary>
    public virtual ICollection<Reminder> Reminders { get; set; }

    /// <summary>
    /// Gets or sets the collection of user-event relationships associated with the event.
    /// </summary>
    public virtual ICollection<UserEvent> UserEvents { get; set; }
}

