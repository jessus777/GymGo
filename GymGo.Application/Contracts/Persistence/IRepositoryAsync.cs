using System.Linq.Expressions;

namespace GymGo.Application.Contracts.Persistence
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<TResult>> GetAsyncWithFilters<TEntity, TResult>(
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, TResult>>? selector = null
            ) where TEntity : class;
        Task<TResult?> GetByIdAsyncWithFilters<TEntity, TResult>(
            Guid id, 
            Expression<Func<TEntity, bool>>? filter = null,
            Expression<Func<TEntity, TResult>>? selector = null
            ) where TEntity : class;
    }
}
