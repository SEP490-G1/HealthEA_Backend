namespace Domain.Models.Entities;
public class Reminder
{
    public Guid ReminderId { get; set; }  
    public Guid EventId { get; set; }  
    public DateTime ReminderTime { get; set; }  
    public string? Message { get; set; } 
}
