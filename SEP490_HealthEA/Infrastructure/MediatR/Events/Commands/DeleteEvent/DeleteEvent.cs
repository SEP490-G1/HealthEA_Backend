using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.DeleteEvent;

public class DeleteEventCommand : IRequest<Unit>
{
    public Guid EventId { get; set; }   
}
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly SqlDBContext _context;

    public DeleteEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
                .Include(e => e.UserEvents)  
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        _context.UserEvents.RemoveRange(eventEntity.UserEvents);

        // Xóa sự kiện
        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
