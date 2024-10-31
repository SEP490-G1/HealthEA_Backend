using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Reminders.Commands.UpdateReminder;

public class UpdateReminderCommand : IRequest<Guid>
{
    public Guid ReminderId { get; set; }
    public DateTime ReminderTime { get; set; }
    public string Message { get; set; } = string.Empty;
}
public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateReminderCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
            throw new Exception(ErrorCode.REMINDER_NOT_FOUND);

        reminder.ReminderTime = request.ReminderTime;
        reminder.Message = request.Message;

        await _context.SaveChangesAsync(cancellationToken);

        return request.ReminderId;
    }
}
