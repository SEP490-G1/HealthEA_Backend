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
        var deviceToken = "dQU0n_tLRUzWpCtL1-VcRO:APA91bEGF5tx0Pb7AqTLK_SkEgi5cqPYYjNmad0expWSI1ul66kSc6N3-pQ0X-LwlXVPHSDwzPn4RAKVF5D-cV_NDoXao7Kt54oqf9_2rCwZ3yB6Vfr3fqU";

        if (deviceToken != null)
        {
            var title = "Thông báo mới!";
            var body = notice.Message;

            await _firebaseService.SendNotificationAsync(deviceToken, title, body);
        }
    }
}

