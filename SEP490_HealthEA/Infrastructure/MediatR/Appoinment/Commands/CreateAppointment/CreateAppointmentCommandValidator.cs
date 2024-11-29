using FluentValidation;

namespace Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.")
                .NotEqual(Guid.Empty).WithMessage("DoctorId cannot null");

        RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId is required.")
                    .NotEqual(Guid.Empty).WithMessage("UserId cannot null");
    }
}
