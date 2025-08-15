using Dapper;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Requests.DateTables;
using GymGo.Application.Responses;
using GymGo.Persistence.Helpers.SqlFileLoader;
using System.Data;

namespace GymGo.Persistence.Repositories
{
    public class DapperDataTableQueryRepositoryAsync
        : IDapperDataTableQueryRepositoryAsync
    {
        private readonly IDbConnection _connection;
        private readonly ISqlFileLoader _sqlFileLoader;

        public DapperDataTableQueryRepositoryAsync(
            IDbConnection connection,
            ISqlFileLoader sqlFileLoader
            )
        {
            _connection = connection;
            _sqlFileLoader = sqlFileLoader;
        }

        public async Task<DataTableResponse<T>> QueryDataTableAsync<T>(
            string sqlFilePath,
            DataTableRequest request,
            object? additionalParameters = null,
            CancellationToken cancellationToken = default)
            where T : class
        {

            var sql = await _sqlFileLoader.LoadSqlAsync("MembershipType", sqlFilePath);
            var allowedOrderColumns = new[] { "Name", "Price", "DurationInDays", "CreatedBy" };
            var orderColumn = request.Columns != null && request.Order != null && request.Order.Count > 0
                ? request.Columns[request.Order[0].Column].Data
                : "Name";
            if (!allowedOrderColumns.Contains(orderColumn))
                orderColumn = "Name";

            var orderDir = request.Order != null && request.Order.Count > 0
                ? request.Order[0].Dir.ToUpper()
                : "ASC";
            if (orderDir != "ASC" && orderDir != "DESC")
                orderDir = "ASC";

            // Reemplazo de placeholders en el SQL
            sql = sql.Replace("{OrderBy}", $"\"{orderColumn}\"")
                     .Replace("{OrderDir}", orderDir)
                     .Replace("{Offset}", request.Start.ToString())
                     .Replace("{Limit}", request.Length.ToString());

            var dynamicParams = new DynamicParameters(additionalParameters);

            dynamicParams.Add("SearchValue",
               string.IsNullOrWhiteSpace(request.Search?.Value) ? null : $"%{request.Search.Value}%",
               DbType.String);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using var multi = await _connection.QueryMultipleAsync(sql, dynamicParams);
            var data = (await multi.ReadAsync<T>()).ToList();
            var recordsTotal = (await multi.ReadAsync<int>()).FirstOrDefault();
            var recordsFiltered = (await multi.ReadAsync<int>()).FirstOrDefault();

            return new DataTableResponse<T>
            {
                Draw = request.Draw,
                Data = data,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered
            };
        }
    }
}
