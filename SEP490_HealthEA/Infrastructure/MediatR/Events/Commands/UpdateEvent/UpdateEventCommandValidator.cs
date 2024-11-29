using Domain.Common.Exceptions;
using FluentValidation;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateAllEventCommandValidator : AbstractValidator<UpdateAllEventCommand>
{
    public UpdateAllEventCommandValidator()
    {
        RuleFor(x => x.EventId)
               .NotEmpty().WithMessage(ErrorCode.EVENT_ID_IS_NOT_EMPTY);
    }
}
