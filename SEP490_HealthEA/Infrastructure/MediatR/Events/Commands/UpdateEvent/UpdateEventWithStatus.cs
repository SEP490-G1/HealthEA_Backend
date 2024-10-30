using Domain.Common.Exceptions;
using Domain.Enum;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventWithStatus : IRequest<bool>
{
    public Guid? EventId { get; set; }
    public Guid? UserEventId { get; set; }
    public string Action { get; set; }
    public bool? IsAccepted { get; set; }
}

public class UpdateEventWithStatusCommandHandler : IRequestHandler<UpdateEventWithStatus, bool>
{
    private readonly SqlDBContext _context;

    public UpdateEventWithStatusCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateEventWithStatus request, CancellationToken cancellationToken)
    {
        if (request.EventId.HasValue)
        {
            var eventEntity = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

            if (eventEntity == null)
            {
                throw new Exception(ErrorCode.EVENT_NOT_FOUND);
            }

            if (DateTime.UtcNow > eventEntity.EventDateTime)
            {
                eventEntity.Status = EventStatusConstants.Past;
            }
            else
            {
                switch (request.Action)
                {
                    case "Accept":
                        eventEntity.Status = EventStatusConstants.Upcoming;
                        break;
                    case "Reject":
                        eventEntity.Status = EventStatusConstants.Cancelled;
                        break;
                    default:
                        throw new ArgumentException("Invalid action. Action must be either 'Accept' or 'Reject'.");
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        if (request.UserEventId.HasValue && request.IsAccepted.HasValue)
        {
            var userEvent = await _context.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserEventId == request.UserEventId, cancellationToken);

            if (userEvent == null)
            {
                throw new Exception(ErrorCode.USER_EVENT_NOT_FOUND);
            }

            userEvent.IsAccepted = request.IsAccepted.Value;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
