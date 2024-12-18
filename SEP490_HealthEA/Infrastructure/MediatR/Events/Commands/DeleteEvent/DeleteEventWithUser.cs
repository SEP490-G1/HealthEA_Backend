﻿using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.DeleteEvent;

public class DeleteEventWithUserCommand : IRequest<bool>
{
    public Guid EventId { get; set; }
}

public class DeleteEventWithUserCommandHandler : IRequestHandler<DeleteEventWithUserCommand, bool>
{
    private readonly SqlDBContext _context;

    public DeleteEventWithUserCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteEventWithUserCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var eventEntity = await _context.Events
            .Include(e => e.Reminders)  
            .Include(e => e.UserEvents) 
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        _context.Reminders.RemoveRange(eventEntity.Reminders);

        _context.UserEvents.RemoveRange(eventEntity.UserEvents);

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
