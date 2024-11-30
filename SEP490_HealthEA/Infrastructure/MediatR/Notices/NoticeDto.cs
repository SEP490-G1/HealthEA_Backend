namespace Infrastructure.MediatR.Notices;

public class NoticeDto
{
    public Guid NoticeId { get; set; }
    public string? Message { get; set; }
    public string? SenderName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasViewed { get; set; }
}
