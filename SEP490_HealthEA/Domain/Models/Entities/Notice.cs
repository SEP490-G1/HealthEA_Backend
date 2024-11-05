namespace Domain.Models.Entities;

public class Notice
{
    public Guid NoticeId { get; set; }
    public Guid UserId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
}
