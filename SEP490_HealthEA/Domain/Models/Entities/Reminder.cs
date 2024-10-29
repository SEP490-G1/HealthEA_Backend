using Domain.Enum;

namespace Domain.Models.Entities;
public class Reminder
{
    public Guid ReminderId { get; set; }  
    public Guid EventId { get; set; }  
    public int ReminderOffset { get; set; }
    public OffsetUnitContants OffsetUnit {  get; set; }
    public DateTime ReminderTime { get; set; }  
    public string? Message { get; set; }
    public bool IsSent { get; set; } = false;
}
