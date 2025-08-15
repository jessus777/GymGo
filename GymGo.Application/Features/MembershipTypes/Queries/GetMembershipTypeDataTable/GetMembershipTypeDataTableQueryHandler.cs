using FluentResults;
using GymGo.Application.Contracts;
using GymGo.Application.Dtos;
using GymGo.Application.Requests;
using GymGo.Application.Responses;

namespace GymGo.Application.Features.MembershipTypes.Queries.GetMembershipTypeDataTable
{
    public sealed class GetMembershipTypeDataTableQueryHandler
        : ApplicationRequestHandler<GetMembershipTypeDataTableQuery, DataTableResponse<MembershipTypeDto>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GetMembershipTypeDataTableQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override async Task<Result<DataTableResponse<MembershipTypeDto>>> HandleAsync(
            GetMembershipTypeDataTableQuery request,
            CancellationToken cancellationToken
            )
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var membershipTypeTableFiltered = await unitOfWork.MembershipTypeUnitOfWork.MembershipTypeDapperQueriesRepositoryAsync
                .GetDataTableAsync(request.DataTableRequest, cancellationToken);
            return Ok(membershipTypeTableFiltered);
        }
    }
}
