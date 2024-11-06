namespace Infrastructure.MediatR.Events.Queries;

using global::MediatR;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

public class GetEventAllQuery : IRequest<GetEventResponse>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class GetEventResponse
{
    public List<ListDayDto> ListDay { get; set; } = new List<ListDayDto>();
    public List<ListRemindDto> ListRemind { get; set; } = new List<ListRemindDto>();
}

public class ListDayDto
{
    public string DateTime { get; set; }
    public List<ReminderDto> Reminders { get; set; } = new List<ReminderDto>();
}

public class ListRemindDto
{
    public Guid RemindID { get; set; }
}

public class ReminderDto
{
    public Guid RemindID { get; set; }
    public string Time { get; set; }
}

public class GetEventAllQueryyHandler : IRequestHandler<GetEventAllQuery, GetEventResponse>
{
    private readonly SqlDBContext _context;

    public GetEventAllQueryyHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<GetEventResponse> Handle(GetEventAllQuery request, CancellationToken cancellationToken)
    {
        var reminders = await _context.Reminders
            .Where(r => r.ReminderTime >= request.StartDate && r.ReminderTime <= request.EndDate)
            .Select(r => new
            {
                r.ReminderId,
                r.ReminderTime
            })
            .ToListAsync(cancellationToken);

        var groupedReminders = reminders
            .GroupBy(r => r.ReminderTime.Date)
            .Select(g => new ListDayDto
            {
                DateTime = g.Key.ToString("dd-MM-yyyy"),
                Reminders = g.Select(rem => new ReminderDto
                {
                    RemindID = rem.ReminderId,
                    Time = rem.ReminderTime.ToString("HH:mm:ss")
                }).ToList()
            })
            .ToList();

        var response = new GetEventResponse
        {
            ListDay = groupedReminders,
            ListRemind = reminders.Select(r => new ListRemindDto { RemindID = r.ReminderId }).ToList()
        };

        return response;
    }
}