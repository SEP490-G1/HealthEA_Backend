using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Reminders.Commands.DeleteReminder;

public class DeleteReminderCommand : IRequest<bool>
{
    public Guid ReminderId { get; set; }
}
public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, bool>
{
    private readonly SqlDBContext _context;

    public DeleteReminderCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            throw new Exception(ErrorCode.REMINDER_NOT_FOUND);
        }
        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
