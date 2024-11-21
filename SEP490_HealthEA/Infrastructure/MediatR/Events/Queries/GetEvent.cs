using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GeEventWithIDQuery : IRequest<EventDto>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
}

public class GetEventQueryHandler : IRequestHandler<GeEventWithIDQuery, EventDto>
{
    private readonly SqlDBContext _context;

    public GetEventQueryHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<EventDto> Handle(GeEventWithIDQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
     .Include(e => e.Reminders)
     .Where(e => e.EventId == request.EventId && e.UserEvents.Any(ue => ue.UserId == request.UserId))
     .Select(e => new EventDto
     {
         EventId = e.EventId,
         OriginalEventId = e.OriginalEventId,
         Title = e.Title,
         Description = e.Description,
         EventDateTime = e.EventDateTime,
         StartTime = e.StartTime,
         EndTime = e.EndTime,
         Location = e.Location,
         Status = e.Status,
         Type = e.Type,
         RepeatFrequency = e.RepeatFrequency,
         //RepeatInterval = e.RepeatInterval,
         RepeatEndDate = e.RepeatEndDate,
         ReminderOffsetDtos = e.Reminders.Select(r => new ReminderOffsetDto
         {
             OffsetUnit = r.OffsetUnit,
             OffsetValue = r.ReminderOffset
         }).ToList()
     })
     .FirstOrDefaultAsync(cancellationToken);

        return eventEntity;

    }


}
