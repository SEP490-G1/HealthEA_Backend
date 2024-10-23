namespace Domain.Models.Entities;
public class Event
{
    public Guid EventId { get; set; } 
    public Guid UserId { get; set; }  
    public string? Title { get; set; }  
    public string? Description { get; set; } 
    public DateTime EventDateTime { get; set; } 
    public string? Location { get; set; } 
    public Guid StatusId { get; set; }  
    public DateTime CreatedAt { get; set; }  
    public Guid CreatedBy { get; set; } 
    public DateTime? UpdatedAt { get; set; }  
    public Guid? UpdatedBy { get; set; } 
}

