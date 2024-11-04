using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventWithCancel : IRequest<List<EventDto>>
{
}
public class GetEventWithCancelHandler : IRequestHandler<GetEventWithCancel, List<EventDto>>
{
    private readonly SqlDBContext _context;

    public GetEventWithCancelHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> Handle(GetEventWithCancel request, CancellationToken cancellationToken)
    {
        var cancelledEvents = await _context.Events
            .Where(e => e.Status == EventStatusConstants.Cancelled)
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

        return cancelledEvents;
    }
}