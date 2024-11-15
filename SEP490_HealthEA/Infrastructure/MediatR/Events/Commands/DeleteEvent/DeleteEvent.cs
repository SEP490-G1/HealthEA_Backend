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
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
            .Include(e => e.Reminders) // Include related reminders to delete them as well
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);

        // Remove the event and its related reminders
        _context.Events.Remove(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
