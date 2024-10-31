namespace Infrastructure.MediatR.Events.Queries;

public class UserEventDto
{
    public Guid UserEventId { get; set; }
    public Guid UserId { get; set; }
    public bool? IsAccepted { get; set; }
    public bool IsOrganizer { get; set; }
}
