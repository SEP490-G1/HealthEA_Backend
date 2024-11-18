using Domain.Enum;
using System;

namespace Domain.Models.Entities;

/// <summary>
/// Represents an event with details such as title, time, location, and recurrence settings.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();


    /// <summary>
    /// Gets or sets the unique identifier of the original event if this is a recurring or derived event.
    /// </summary>
    public Guid? OriginalEventId { get; set; } = Guid.NewGuid();


    /// <summary>
    /// Gets or sets the username of the person associated with the event.
    /// </summary>
    public string? UserName { get; set; }


    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public string? Title { get; set; }


    /// <summary>
    /// Gets or sets the description or details of the event.
    /// </summary>
    public string? Description { get; set; }


    /// <summary>
    /// Gets or sets the date and time of the event.
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
    /// Gets or sets the location where the event will take place.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>1</c>.
    /// </remarks>
    public int Type { get; set; } = 1;

    /// <summary>
    /// Gets or sets the status of the event.
    /// </summary>
    public EventStatusConstants? Status { get; set; }

    /// <summary>
    /// Gets or sets the frequency at which the event repeats.
    /// </summary>
    public EventDailyConstants RepeatFrequency { get; set; } // Tần suất lặp lại

    /// <summary>
    /// Gets or sets the interval between repeated events.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>1</c>.
    /// </remarks>
    public int RepeatInterval { get; set; } = 1; // Khoảng thời gian lặp lại


    /// <summary>
    /// Gets or sets the end date for the recurring event.
    /// </summary>
    public DateTime RepeatEndDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event was created.
    /// </summary>
    /// <remarks>
    /// Defaults to the current UTC time.
    /// </remarks>
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the username of the creator of the event.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the username of the last person who updated the event.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the collection of reminders associated with the event.
    /// </summary>
    public virtual ICollection<Reminder> Reminders { get; set; }

    /// <summary>
    /// Gets or sets the collection of user-event associations.
    /// </summary>
    public virtual ICollection<UserEvent> UserEvents { get; set; }

}

