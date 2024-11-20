namespace Domain.Models.Entities;

/// <summary>
/// Represents a notice sent to a user, containing a message and recipient information.
/// </summary>
public class Notice
{
    /// <summary>
    /// Gets or sets the unique identifier for the notice.
    /// </summary>
    public Guid NoticeId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the user who created the notice.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the recipient of the notice.
    /// </summary>
    public Guid RecipientId { get; set; }

    /// <summary>
    /// Gets or sets the content of the notice message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the notice was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the notice.
    /// </summary>
    public User Users { get; set; }
}
