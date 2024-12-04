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
//var reminderTimes = new List<DateTime>
//{
//    DateTime.Today.AddHours(10).AddMinutes(57),
//    DateTime.Today.AddHours(10).AddMinutes(58),
//    DateTime.Today.AddHours(09).AddMinutes(07),
//    DateTime.Today.AddHours(09).AddMinutes(08),
//    DateTime.Today.AddHours(09).AddMinutes(11),
//    DateTime.Today.AddHours(09).AddMinutes(18),
//    DateTime.Today.AddHours(09).AddMinutes(19),
//    DateTime.Today.AddHours(09).AddMinutes(07)
//};