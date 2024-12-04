using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
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
    public List<ReminderOffsetDto>? ReminderOffsets { get; set; }
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
        var userEvent = await _context.UserEvents
           .FirstOrDefaultAsync(ue => ue.EventId == request.EventId && ue.UserId == request.UserId, cancellationToken);
        if (userEvent == null)
        {
            throw new Exception(ErrorCode.UNAUTHORIZED_ACCESS);
        }
        eventEntity.Title = request.Title ?? eventEntity.Title;
        eventEntity.Description = request.Description ?? eventEntity.Description;
        eventEntity.EventDateTime = request.EventDateTime ?? eventEntity.EventDateTime;
        eventEntity.StartTime = request.StartTime ?? eventEntity.StartTime;
        eventEntity.EndTime = request.EndTime ?? eventEntity.EndTime;
        eventEntity.Location = request.Location ?? eventEntity.Location;

        if (request.ReminderOffsets != null && request.ReminderOffsets.Any())
        {
            _context.Reminders.RemoveRange(eventEntity.Reminders);

            var eventStartTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime);

            var newReminders = request.ReminderOffsets.Select(r =>
            {
                var reminderTime = r.OffsetUnit switch
                {
                    OffsetUnitContants.minutes => eventStartTime.AddMinutes(-r.OffsetValue), 
                    OffsetUnitContants.hours => eventStartTime.AddHours(-r.OffsetValue),  
                    OffsetUnitContants.days => eventStartTime.AddDays(-r.OffsetValue),
                    //OffsetUnitContants.weeks => eventStartTime.AddDays(-r.OffsetValue),
                    _ => eventStartTime 
                };

                return new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventEntity.EventId,
                    OffsetUnit = r.OffsetUnit,
                    ReminderTime = reminderTime,
                    ReminderOffset = r.OffsetValue,
                    Message = $"Reminder for event: {eventEntity.Title}"
                };
            }).ToList();

            await _context.Reminders.AddRangeAsync(newReminders, cancellationToken);
        }
        else
        {
            _context.Reminders.RemoveRange(eventEntity.Reminders);
            var defaultReminder = new Reminder
            {
                ReminderId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                ReminderTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime),  
                Message = $"Reminder for event: {eventEntity.Title}"
            };

            await _context.Reminders.AddAsync(defaultReminder, cancellationToken);
        }


        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }

}
