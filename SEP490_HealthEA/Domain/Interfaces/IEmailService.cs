namespace Domain.Interfaces
{
    public interface IEmailService : INotificationService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
