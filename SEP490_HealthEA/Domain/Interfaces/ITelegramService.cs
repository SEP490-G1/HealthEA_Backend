namespace Domain.Interfaces
{
    public interface ITelegramService : INotificationService
    {
        Task SendMessageAsync(string message);
    }

}
