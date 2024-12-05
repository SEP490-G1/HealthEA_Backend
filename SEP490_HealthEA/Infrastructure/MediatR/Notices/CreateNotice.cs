using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Google;
using Infrastructure.SQLServer;
using MediatR;

namespace Infrastructure.MediatR.Notices;

public class CreateNoticeCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
}
public class CreateNoticeCommandHandler : IRequestHandler<CreateNoticeCommand, string>
{
    private readonly INoticeService _noticeService;

	public CreateNoticeCommandHandler(INoticeService noticeService)
	{
		_noticeService = noticeService;
	}

	public async Task<string> Handle(CreateNoticeCommand request, CancellationToken cancellationToken)
    {
        var notice = new Notice
        {
            NoticeId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipientId = request.RecipientId,
            Message = request.Message,
            CreatedAt = DateTime.Now
        };

        await _noticeService.CreateAndSendNoticeAsync(notice, "Thông báo mới!");

        return "Notice created and notification sent!";
    }
   
}
