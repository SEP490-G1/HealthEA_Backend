using Quartz;

namespace Infrastructure.Notification;

public class ReminderJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Thông báo cho Mạnh và Dương: Đã đến thời gian hẹn lúc 8:30!");
        return Task.CompletedTask;
    }
}
