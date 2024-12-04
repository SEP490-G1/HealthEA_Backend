using Infrastructure.Services;
using Infrastructure.SQLServer;
using Quartz;

namespace Infrastructure.Notification;

public class ReminderJob : IJob
{
    private readonly EmailService _emailService;
    private readonly SqlDBContext _context;

    public ReminderJob(EmailService emailService, SqlDBContext context)
    {
        _emailService = emailService;
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var currentTime = DateTime.Now;

        var reminders = _context.Reminders
            .Where(r => r.ReminderTime <= currentTime &&
                        r.ReminderTime > currentTime.AddMinutes(-1)) // Sai số 1 phút
            .ToList();
        if (reminders.Count > 0)
        {
            foreach (var reminder in reminders)
            {
                Guid reminderId = reminder.ReminderId;
                var email = new Email
                {
                    SenderEmail = "doan24fa@gmail.com",
                    SenderPassword = "aeay nhir mlgk nazg", // Bảo mật mật khẩu
                    Subject = $"Reminder for event",
                    Body = reminder.Message
                };


                _emailService.SendEmailToAllUsers(email, reminderId);

                reminder.IsSent = true;
            }
        }
       

        await _context.SaveChangesAsync();

    }
}
