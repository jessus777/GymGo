using Dapper;
using GymGo.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GymGo.Persistence.Repositories
{
    public class DapperRepositoryAsync<T>
        : IDapperRepositoryAsync<T> where T : class
    {
        protected readonly IDbConnection _connection;
        public DapperRepositoryAsync(
            IDbConnection connection
            )
        {
            _connection = connection;
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            return await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T?> QueryFirstOrDefaultAsync(string sql, object? parameters = null)
        {
            var query = await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            return query;
        }
    }
}
