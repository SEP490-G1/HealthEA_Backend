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
         var deviceToken = "dYKLgmgMJYiCA4C7WfpC-_:APA91bEP06nxunuEwh8xdM3ruCdTJVq1TpN3MEfmbOKYW0BgydEg02Pkdq-NfaSlHpq2NolwcY18FTW8rVhHp-yQ0uLHgJWRhp1iEZg2ysrsF-hTxSDURGo";

        if (deviceToken != null)
        {
            var title = "Thông báo mới!";
            var body = notice.Message;

            await _firebaseService.SendNotificationAsync(deviceToken, title, body);
        }
    }
}

