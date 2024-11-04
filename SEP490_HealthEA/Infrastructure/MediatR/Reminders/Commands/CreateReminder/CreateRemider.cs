using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Reminders.Commands.CreateReminder;
public class CreateReminderCommand : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public DateTime ReminderTime { get; set; }
    public string Message { get; set; } = string.Empty;
}
public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, Guid>
{
    private readonly SqlDBContext _context;

    public CreateReminderCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventExists = await _context.Events.AnyAsync(e => e.EventId == request.EventId, cancellationToken);
        if (!eventExists)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            EventId = request.EventId,
            ReminderTime = request.ReminderTime,
            Message = request.Message,
            IsSent = false 
        };

        await _context.Reminders.AddAsync(reminder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return reminder.ReminderId;
    }
}
