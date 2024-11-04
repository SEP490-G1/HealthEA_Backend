using Domain.Enum;
namespace Domain.Models.Entities;

public class Reminder
{
    public Guid ReminderId { get; set; } = Guid.NewGuid();
    public Guid EventId { get; set; }
    public int ReminderOffset { get; set; }  // Integer for reminder offset
    public OffsetUnitContants OffsetUnit { get; set; }  // Unit of time for the offset
    public DateTime ReminderTime { get; set; }  // Calculated reminder time based on ReminderOffset and EventDateTime
    public string? Message { get; set; }
    public bool IsSent { get; set; } = false;
    public virtual Event Events { get; set; }
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
