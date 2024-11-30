using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Notices;
public class NoticeCreatedEvent : INotification
{
    public Notice Notice { get; }

    public NoticeCreatedEvent(Notice notice)
    {
        Notice = notice;
    }
}
public class NoticeCreatedEventHandler : INotificationHandler<NoticeCreatedEvent>
{
    private readonly FirebaseNotificationService _firebaseService;
    private readonly SqlDBContext _context;

    public NoticeCreatedEventHandler(FirebaseNotificationService firebaseService, SqlDBContext context)
    {
        _firebaseService = firebaseService;
        _context = context;
    }

    public async Task Handle(NoticeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var notice = notification.Notice;
        var tokens = await _context.DeviceTokens
            .Where(t => t.UserId == notice.RecipientId)
            .Select(t => t.DeviceToken)
            .ToListAsync();
        if (tokens != null && tokens.Any())
        {
            foreach(var token in tokens)
            {
				var title = "Thông báo mới!";
				var body = notice.Message;

				await _firebaseService.SendNotificationAsync(token, title, body);
			}
        }
    }
}

