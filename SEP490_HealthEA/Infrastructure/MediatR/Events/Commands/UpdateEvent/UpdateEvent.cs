using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? EventDateTime { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
            .Include(e => e.Reminders)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        eventEntity.Title = request.Title ?? eventEntity.Title;
        eventEntity.Description = request.Description ?? eventEntity.Description;
        eventEntity.EventDateTime = request.EventDateTime ?? eventEntity.EventDateTime;
        eventEntity.StartTime = request.StartTime ?? eventEntity.StartTime;
        eventEntity.EndTime = request.EndTime ?? eventEntity.EndTime;
        eventEntity.Location = request.Location ?? eventEntity.Location;

        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }

}
