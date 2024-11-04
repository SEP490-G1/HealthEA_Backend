using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventWithPending : IRequest<List<EventDto>>
{
}
public class GetEventWithPendingHandler : IRequestHandler<GetEventWithPending, List<EventDto>>
{
    private readonly SqlDBContext _context;

    public GetEventWithPendingHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> Handle(GetEventWithPending request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var pendingEvents = await _context.Events
            .Where(e => e.Status == EventStatusConstants.Pending &&
                        e.UserEvents.Any(ue => ue.IsAccepted == null))
            .Select(e => new EventDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventDateTime = e.EventDateTime,
                Location = e.Location,
                UserEvents = e.UserEvents.Select(ue => new UserEventDto
                {
                    UserEventId = ue.UserEventId,
                    UserId = ue.UserId,
                    IsAccepted = ue.IsAccepted
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return pendingEvents;
    }
}