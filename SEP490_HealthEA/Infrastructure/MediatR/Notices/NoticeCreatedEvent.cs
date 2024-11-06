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

        //var deviceToken = await _context.DeviceTokens
        //    .Where(dt => dt.UserId == notice.RecipientId)
        //    .Select(dt => dt.DeviceToken)
        //    .FirstOrDefaultAsync(cancellationToken);
        var deviceToken = "ee25qzlTnOJGR5A3rGUfX3:APA91bHYw0aY69neaJqaGyJKUMc5O2R0RD_8-fyk9HpIrj42UWG7zqqs62kgxh_eL-NI1BFtW1EGNCA7SjMsuSJscqGNh5YI_koez0Q7WsV6_r3ph9PrcE8";

        if (deviceToken != null)
        {
            var title = "Thông báo mới!";
            var body = notice.Message;

            await _firebaseService.SendNotificationAsync(deviceToken, title, body);
        }
    }
}

