using Infrastructure.SQLServer;
namespace Infrastructure.Notification;
public class ReminderService
{
    private readonly SqlDBContext _context;

    public ReminderService(SqlDBContext context)
    {
        _context = context;
    }

    public List<DateTime> GetReminderTimes()
    {
        var reminderTimes = _context.Reminders
                                    .Select(r => r.ReminderTime)
                                    .ToList();
        return reminderTimes;
    }
}
