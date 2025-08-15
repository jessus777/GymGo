using GymGo.Application.Requests.DateTables;
using GymGo.Application.Responses;

namespace GymGo.Application.Contracts.Persistence
{
    public interface IDapperDataTableQueryRepositoryAsync
    {
        Task<DataTableResponse<T>> QueryDataTableAsync<T>(
                string sqlFilePath,
                DataTableRequest request,
                object? additionalParameters = null,
                CancellationToken cancellationToken = default
            ) where T : class;
    }
}
