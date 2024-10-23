namespace Domain.Models.Entities;
public class Status
{
    public Guid StatusId { get; set; }  
    public string? StatusName { get; set; }
    public ICollection<Event> Events { get; set; }
}
