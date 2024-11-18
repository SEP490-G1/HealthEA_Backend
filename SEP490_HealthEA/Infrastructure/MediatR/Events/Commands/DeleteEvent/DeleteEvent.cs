using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.DeleteEvent;

public class DeleteEventCommand : IRequest<bool>
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly SqlDBContext _context;

    public DeleteEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
                .Include(e => e.Reminders)
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);

        var userEvents = await _context.UserEvents
            .Where(ue => ue.EventId == request.EventId && ue.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        if (userEvents.Count ==0)
        {
            throw new Exception(ErrorCode.UNAUTHORIZED_ACCESS);
        }
        _context.UserEvents.RemoveRange(userEvents);
        await _context.SaveChangesAsync(cancellationToken);

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
