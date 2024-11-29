using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.DeleteEvent;

public class DeleteAllEventCommand : IRequest<bool>
{
    public Guid OriginalEventId { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
}



public class DeleteAllEventCommandHandler : IRequestHandler<DeleteAllEventCommand, bool>
{
    private readonly SqlDBContext _context;

    public DeleteAllEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAllEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventsToDelete = await _context.Events
            .Where(e => e.OriginalEventId == request.OriginalEventId)
            .Include(e => e.Reminders)
            .ToListAsync(cancellationToken);

        if (!eventsToDelete.Any())
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        var eventIdsToDelete = eventsToDelete.Select(e => e.EventId).ToList();

        var userEvents = await _context.UserEvents
            .Where(ue => eventIdsToDelete.Contains(ue.EventId) && ue.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        if (userEvents.Count == 0)
        {
            throw new Exception(ErrorCode.UNAUTHORIZED_ACCESS);
        }

        _context.UserEvents.RemoveRange(userEvents);

        foreach (var eventToDelete in eventsToDelete)
        {
            _context.Reminders.RemoveRange(eventToDelete.Reminders);
        }

        _context.Events.RemoveRange(eventsToDelete);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }


}
