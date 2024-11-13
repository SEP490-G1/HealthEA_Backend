using Domain.Common.Exceptions;
using FluentValidation;

namespace Infrastructure.MediatR.Reminders.Commands.CreateReminder;

public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
{
    public CreateReminderCommandValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage(ErrorCode.EVENT_ID_IS_NOT_EMPTY);
    }
}