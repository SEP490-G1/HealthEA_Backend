using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventsQuery : IRequest<List<EventDto>>
{
    public Guid UserId { get; set; } = Guid.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventDto>>
{
    private readonly SqlDBContext _context;

    public GetEventsQueryHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _context.Events
            .Where(e => e.UserEvents.Any(ue => ue.UserId == request.UserId) && e.EventDateTime >= request.StartDate && e.EventDateTime <= request.EndDate)
            .Select(e => new EventDto
            {
                EventId = e.EventId,
                //UserName = e.UserName,
                Title = e.Title,
                Description = e.Description,
                EventDateTime = e.EventDateTime,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Location = e.Location,
                Status = e.Status,
                Type = e.Type,
                RepeatFrequency = e.RepeatFrequency,
                RepeatInterval = e.RepeatInterval,
                RepeatEndDate = e.RepeatEndDate
            })
            .ToListAsync(cancellationToken);

        return events;
    }
}
