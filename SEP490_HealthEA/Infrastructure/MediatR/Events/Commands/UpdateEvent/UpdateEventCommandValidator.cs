using Domain.Common.Exceptions;
using FluentValidation;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator()
    {
        RuleFor(x => x.EventId)
               .NotEmpty().WithMessage(ErrorCode.EVENT_ID_IS_NOT_EMPTY);
    }
}
