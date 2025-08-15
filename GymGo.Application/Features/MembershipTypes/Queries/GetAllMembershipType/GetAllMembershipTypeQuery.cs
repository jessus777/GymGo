using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetAllMembershipType
{
    public sealed class GetAllMembershipTypeQuery
        : ApplicationRequest<List<MembershipTypeDto>>
    {
    }
}
