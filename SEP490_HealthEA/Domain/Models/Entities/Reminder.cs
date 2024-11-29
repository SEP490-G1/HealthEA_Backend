using Domain.Enum;
namespace Domain.Models.Entities;

/// <summary>
/// Represents a reminder for an event, including the time, offset, and message.
/// </summary>
public class Reminder
{
    /// <summary>
    /// Gets or sets the unique identifier for the reminder.
    /// Default value is a new GUID.
    /// </summary>
    public Guid ReminderId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the unique identifier of the associated event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the offset for the reminder in the specified unit (e.g., minutes, hours, days).
    /// </summary>
    public int ReminderOffset { get; set; }  // Integer for reminder offset

    /// <summary>
    /// Gets or sets the unit of time used for the offset.
    /// </summary>
    public OffsetUnitContants OffsetUnit { get; set; }  // Unit of time for the offset

    /// <summary>
    /// Gets or sets the calculated reminder time based on the event's date and offset.
    /// </summary>
    public DateTime ReminderTime { get; set; }  // Calculated reminder time based on ReminderOffset and EventDateTime

    /// <summary>
    /// Gets or sets an optional message for the reminder.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the reminder has been sent.
    /// Default value is false.
    /// </summary>
    public bool IsSent { get; set; } = false;

    /// <summary>
    /// Gets or sets the associated event for the reminder.
    /// </summary>
    public virtual Event Events { get; set; }

    /// <summary>
    /// Calculates the reminder time based on the event's date and the reminder offset.
    /// </summary>
    /// <param name="eventDateTime">The date and time of the event.</param>
    public void CalculateReminderTime(DateTime eventDateTime)
    {
        DateTime offsetTime = eventDateTime;
        switch (OffsetUnit)
        {
            case OffsetUnitContants.minutes:
                offsetTime = eventDateTime - TimeSpan.FromMinutes(ReminderOffset);
                break;
            case OffsetUnitContants.hours:
                offsetTime = eventDateTime - TimeSpan.FromHours(ReminderOffset);
                break;
            case OffsetUnitContants.days:
                offsetTime = eventDateTime - TimeSpan.FromDays(ReminderOffset);
                break;
        }
        ReminderTime = offsetTime;
    }
}
