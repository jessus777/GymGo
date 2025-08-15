using FluentValidation;

namespace GymGo.Application.Features.MembershipTypes.Commands.CreateMembershipType
{
    public class CreateMembershipTypeCommandValidator
        : AbstractValidator<CreateMembershipTypeCommand>
    {
        public CreateMembershipTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es requerida.")
                .MaximumLength(500).WithMessage("La descripción excede los 500 caracteres.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.DurationInDays)
                .GreaterThan(0).WithMessage("Duration in months must be greater than zero.");
        }
    }
}
