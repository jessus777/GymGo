using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetDetailMembershipType
{
    public sealed class GetDetailMembershipTypeQuery
        : ApplicationRequest<MembershipTypeDetailDto>
    {
        public GetDetailMembershipTypeQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
