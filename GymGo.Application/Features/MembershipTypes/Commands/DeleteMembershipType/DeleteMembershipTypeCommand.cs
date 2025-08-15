using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Commands.DeleteMembershipType
{
    public sealed class DeleteMembershipTypeCommand(Guid id)
                : ApplicationRequest<MembershipTypeDto>
    {
        public Guid Id { get; private set; } = id;
    }
}
