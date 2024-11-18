namespace Domain.Models.Entities;

/// <summary>
/// Represents the relationship between a user and an event,
/// including their role and participation status.
/// </summary>
public class UserEvent
{
    /// <summary>
    /// Gets or sets the unique identifier for the UserEvent association.
    /// </summary>
    public Guid UserEventId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the event.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the event associated with the user.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has accepted the invitation to the event.
    /// </summary>
    public bool IsAccepted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is an organizer of the event.
    /// </summary>
    public bool IsOrganizer { get; set; }

    /// <summary>
    /// Gets or sets the user associated with this UserEvent.
    /// </summary>
    public virtual User Users { get; set; }

    /// <summary>
    /// Gets or sets the event associated with this UserEvent.
    /// </summary>
    public virtual Event Events { get; set; }
}
