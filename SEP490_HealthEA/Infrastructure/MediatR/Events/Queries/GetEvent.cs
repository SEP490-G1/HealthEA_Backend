using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Queries;

public class GeEventWithIDQuery : IRequest<Event>
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
}

public class GetEventQueryHandler : IRequestHandler<GeEventWithIDQuery, Event>
{
    private readonly SqlDBContext _context;

    public GetEventQueryHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Event> Handle(GeEventWithIDQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
     .Include(e => e.Reminders)
     .Where(e => e.EventId == request.EventId && e.UserEvents.Any(ue => ue.UserId == request.UserId))
     .Select(e => new Event
     {
         EventId = e.EventId,
         Title = e.Title,
         Description = e.Description,
         EventDateTime = e.EventDateTime,
         StartTime = e.StartTime,
         EndTime = e.EndTime,
         Location = e.Location,
         Status = e.Status,
         RepeatFrequency = e.RepeatFrequency,
         RepeatInterval = e.RepeatInterval,
         RepeatEndDate = e.RepeatEndDate
     })
     .FirstOrDefaultAsync(cancellationToken);

        //if (eventEntity == null)
        //{
        //    throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        //}

        //eventEntity.Reminders = eventEntity.Reminders.Select(r => new Reminder
        //{
        //    ReminderId = r.ReminderId,
        //    EventId = r.EventId,
        //    ReminderOffset = r.ReminderOffset,
        //    OffsetUnit = r.OffsetUnit,
        //    ReminderTime = r.ReminderTime,
        //    Message = r.Message,
        //    IsSent = r.IsSent
        //}).ToList();

        return eventEntity;

    }


}
