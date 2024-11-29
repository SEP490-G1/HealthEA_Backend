using Domain.Common.Exceptions;
using FluentValidation;

namespace Infrastructure.MediatR.Reminders.Commands.UpdateReminder;

public class UpdateReminderCommandValidator : AbstractValidator<UpdateReminderCommand>
{
    public UpdateReminderCommandValidator()
    {
        RuleFor(x => x.ReminderId)
            .NotEmpty().WithMessage(ErrorCode.REMINDER_ID_IS_NOT_EMPTY);
    } 
}
