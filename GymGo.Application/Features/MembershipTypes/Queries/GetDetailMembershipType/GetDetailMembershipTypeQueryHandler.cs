using FluentResults;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Application.Exceptions;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetDetailMembershipType
{
    public sealed class GetDetailMembershipTypeQueryHandler
        : ApplicationRequestHandler<GetDetailMembershipTypeQuery, MembershipTypeDetailDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GetDetailMembershipTypeQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override async Task<Result<MembershipTypeDetailDto>> HandleAsync(
            GetDetailMembershipTypeQuery request,
            CancellationToken cancellationToken
            )
        {
            using var unitOfWork = _unitOfWorkFactory.Create();
            var membershipType = await unitOfWork
                .MembershipTypeUnitOfWork
                .MembershipTypeDapperQueriesRepositoryAsync
                .GetByIdQueryAsync(request.Id, cancellationToken);

            if (membershipType is null)
            {
                return DomainError.EntityNotFound(request.Id);
            }

            return Ok(membershipType);
        }
    }
}
