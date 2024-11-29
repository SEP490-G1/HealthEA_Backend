using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Reminders.Queries;

public class GetReminderQuery : IRequest<Reminder>
{
    public Guid ReminderId { get; set; }
}
public class GetReminderQueryHandler : IRequestHandler<GetReminderQuery, Reminder>
{
    private readonly SqlDBContext _context;

    public GetReminderQueryHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Reminder> Handle(GetReminderQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Tìm reminder dựa trên ReminderId
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(r => r.ReminderId == request.ReminderId, cancellationToken);

        if (reminder == null)
        {
            throw new Exception(ErrorCode.REMINDER_NOT_FOUND);
        }

        return reminder;
    }
}
