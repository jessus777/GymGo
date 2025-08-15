namespace GymGo.Application.Contracts.Persistence
{
    public interface IDapperRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null);
        Task<T?> QueryFirstOrDefaultAsync(string sql, object? parameters = null);
        Task<int> ExecuteAsync(string sql, object? parameters = null);
    }

}
