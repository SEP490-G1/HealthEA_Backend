using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GetEventQuery : IRequest<Event>
{
    public Guid EventId { get; set; }
}

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, Event>
{
    private readonly SqlDBContext _context;

    public GetEventQueryHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Event> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
            .Include(e => e.Reminders) 
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        return eventEntity;
    }
}
