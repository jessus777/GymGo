using FluentResults;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetAllMembershipType
{
    public sealed class GetAllMembershipTypeQueryHandler
        : ApplicationRequestHandler<GetAllMembershipTypeQuery, List<MembershipTypeDto>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GetAllMembershipTypeQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override async Task<Result<List<MembershipTypeDto>>> HandleAsync(
            GetAllMembershipTypeQuery request,
            CancellationToken cancellationToken
            )
        {
            using var unitOfWork = _unitOfWorkFactory.Create();
            var membershipTypes = await unitOfWork
                .MembershipTypeUnitOfWork
                .MembershipTypeDapperQueriesRepositoryAsync
                .GetAllMembershipTypesQueryAsync(cancellationToken);

            return Ok(membershipTypes.ToList());
        }
    }
}
