using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.DeleteEvent;

public class DeleteAllEventCommand : IRequest<bool>
{
    public Guid OriginalEventId { get; set; } = Guid.NewGuid();
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
        try
        {
            var eventsToDelete = await _context.Events
                .Where(e => e.OriginalEventId == request.OriginalEventId)
                .Include(e => e.Reminders)
                .ToListAsync(cancellationToken);

            if (!eventsToDelete.Any())
            {
                throw new Exception(ErrorCode.EVENT_NOT_FOUND);
            }

            foreach (var eventToDelete in eventsToDelete)
            {
                _context.Reminders.RemoveRange(eventToDelete.Reminders);
            }

            _context.Events.RemoveRange(eventsToDelete);

            await _context.SaveChangesAsync(cancellationToken);

            return true; 
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
