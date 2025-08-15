using GymGo.Application.Dtos;
using GymGo.Application.Requests.DateTables;
using GymGo.Application.Responses;

namespace GymGo.Application.Contracts.Persistence
{
    public interface IMembershipTypeDapperQueriesRepositoryAsync
    {
        Task<IEnumerable<MembershipTypeDto>> GetAllMembershipTypesQueryAsync(CancellationToken cancellationToken = default);
        Task<MembershipTypeDetailDto?> GetByIdQueryAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DataTableResponse<MembershipTypeDto>> GetDataTableAsync(DataTableRequest request, CancellationToken cancellationToken = default);
    }
}
