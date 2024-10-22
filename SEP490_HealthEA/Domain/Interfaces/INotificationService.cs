namespace Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendNotification(string recipient, string subject, string message);
    }
}
