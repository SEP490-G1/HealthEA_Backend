using Domain.Common.Exceptions;
using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventWithStatus : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public string Action { get; set; }
}
public class UpdateEventWithStatusCommandHandler : IRequestHandler<UpdateEventWithStatus, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateEventWithStatusCommandHandler(SqlDBContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(UpdateEventWithStatus request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
                 .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        if (request.Action == "Accept")
        {
            eventEntity.Status = EventStatusConstants.Upcoming;
        }
        else if (request.Action == "Reject")
        {
            eventEntity.Status = EventStatusConstants.Cancelled;
        }
        else
        {
            throw new ArgumentException("Invalid action.");
        }

        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }
}
