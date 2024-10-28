namespace Domain.Models.Entities;

public class UserEvent
{
    public Guid UserEventId { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsOrganizer { get; set; }
    public virtual User Users { get; set; }
    public virtual Event Events { get; set; }
}
