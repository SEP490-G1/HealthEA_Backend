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
         var deviceToken = "cCb2Lu8BpcOCI-_2__HLRp:APA91bGMgVHleh_IZaG51BIaZoYXT8PwyV7PQfa6qlJq9KqVr6cl1zK_62dRO50V55AWjAHQaVF5XkMGtYBGvx1TgpoLUHLkJnu_A22T1oi2wNDzZEbo8_Q";

        if (deviceToken != null)
        {
            var title = "Thông báo mới!";
            var body = notice.Message;

            await _firebaseService.SendNotificationAsync(deviceToken, title, body);
        }
    }
}

