using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateIsAcceptedEventCommand : IRequest<bool>
{
    public Guid UserEventId { get; set; }
    public bool IsAccepted { get; set; }
}
public class UpdateIsAcceptedEventCommandHandler : IRequestHandler<UpdateIsAcceptedEventCommand, bool>
{
    private readonly SqlDBContext _context;

    public UpdateIsAcceptedEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateIsAcceptedEventCommand request, CancellationToken cancellationToken)
    {
        var userEvent = await _context.UserEvents
            .FirstOrDefaultAsync(ue => ue.UserEventId == request.UserEventId, cancellationToken);

        if (userEvent == null)
        {
            throw new Exception(ErrorCode.USER_EVENT_NOT_FOUND);
        }

        userEvent.IsAccepted = request.IsAccepted ? true : false;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

