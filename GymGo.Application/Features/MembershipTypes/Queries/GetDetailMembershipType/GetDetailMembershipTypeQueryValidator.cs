using FluentValidation;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetDetailMembershipType
{
    public class GetDetailMembershipTypeQueryValidator
        : AbstractValidator<GetDetailMembershipTypeQuery>
    {
        public GetDetailMembershipTypeQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El id no debe venir vacio.");
        }
    }
}
