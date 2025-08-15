using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Application.Requests.DateTables;
using GymGo.Application.Responses;
using GymGo.Persistence.Helpers.SqlFileLoader;
using System.Data;

namespace GymGo.Persistence.Repositories
{
    public class MembershipTypeDapperQueriesRepositoryAsync
        : IMembershipTypeDapperQueriesRepositoryAsync
    {
        private readonly IDbConnection _connection;
        private readonly ISqlFileLoader _sqlFileLoader;

        public MembershipTypeDapperQueriesRepositoryAsync(
            IDbConnection connection, 
            ISqlFileLoader sqlFileLoader
            )
        {
            _connection = connection;
            _sqlFileLoader = sqlFileLoader;
        }



        public async Task<IEnumerable<MembershipTypeDto>> GetAllMembershipTypesQueryAsync(CancellationToken cancellationToken = default)
        {
            var sql = await _sqlFileLoader.LoadSqlAsync("MembershipType", "GetAllMembershipType.sql");
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var dapperRepository = new DapperRepositoryAsync<MembershipTypeDto>(_connection);
            return await dapperRepository.QueryAsync(sql);
        }

        public async Task<MembershipTypeDetailDto?> GetByIdQueryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sql = await _sqlFileLoader.LoadSqlAsync("MembershipType", "GetDetailMembershipType.sql");
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var dapperRepository = new DapperRepositoryAsync<MembershipTypeDetailDto>(_connection);
            var parameters = new { Id = id };
            return await dapperRepository.QueryFirstOrDefaultAsync(sql, parameters);
        }

        public async Task<DataTableResponse<MembershipTypeDto>> GetDataTableAsync(
            DataTableRequest request, 
            CancellationToken cancellationToken = default
            )
        {
            var dapperRepo = new DapperDataTableQueryRepositoryAsync(_connection, _sqlFileLoader);
            return await dapperRepo.QueryDataTableAsync<MembershipTypeDto>(
                "GetDataTableMembershipType.sql",
                request,
                null,
                cancellationToken
            );
        }
    }
}
