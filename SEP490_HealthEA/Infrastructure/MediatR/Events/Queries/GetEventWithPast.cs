using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventWithPast : IRequest<List<EventDto>>
{
}
public class GetEventWithPastHandler : IRequestHandler<GetEventWithPast, List<EventDto>>
{
    private readonly SqlDBContext _context;

    public GetEventWithPastHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> Handle(GetEventWithPast request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var pastEvents = await _context.Events
            .Where(e => e.EventDateTime < now && e.Status == EventStatusConstants.Past)
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

        return pastEvents;
    }
}
