using Domain.Common;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INotificationService CreateNotificationService(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Telegram:
                    return _serviceProvider.GetRequiredService<ITelegramService>();

                case NotificationType.Email:
                    return _serviceProvider.GetRequiredService<IEmailService>();

                default:
                    throw new ArgumentException("Invalid notification type", nameof(notificationType));
            }
        }
    }
}
