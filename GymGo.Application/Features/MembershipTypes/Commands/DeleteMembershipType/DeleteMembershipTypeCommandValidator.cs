using FluentValidation;

namespace GymGo.Application.Features.MembershipTypes.Commands.DeleteMembershipType
{
    public sealed class DeleteMembershipTypeCommandValidator
        : AbstractValidator<DeleteMembershipTypeCommand>
    {
        public DeleteMembershipTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El id no debe venir vacio.");
        }
    }
}
