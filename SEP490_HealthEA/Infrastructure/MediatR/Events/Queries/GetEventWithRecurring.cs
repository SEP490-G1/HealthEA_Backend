using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventWithRecurring : IRequest<List<EventDto>>
{
}
public class GetEventWithRecurringHandler : IRequestHandler<GetEventWithRecurring, List<EventDto>>
{
    private readonly SqlDBContext _context;

    public GetEventWithRecurringHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> Handle(GetEventWithRecurring request, CancellationToken cancellationToken)
    {
        var recurringEvents = await _context.Events
            .Where(e => e.RepeatFrequency != EventDailyConstants.NotRepeat)
            .Select(e => new EventDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventDateTime = e.EventDateTime,
                Location = e.Location,
                RepeatFrequency = e.RepeatFrequency,
                UserEvents = e.UserEvents.Select(ue => new UserEventDto
                {
                    UserEventId = ue.UserEventId,
                    UserId = ue.UserId,
                    IsAccepted = ue.IsAccepted
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return recurringEvents;
    }
}