using GymGo.Application.Contracts.Persistence;
using GymGo.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymGo.Persistence.Repositories
{
    public class RepositoryAsync<T>
        : IRepositoryAsync<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<TResult>> GetAsyncWithFilters<TEntity, TResult>(
            Expression<Func<TEntity, bool>>? filter = null, 
            Expression<Func<TEntity, TResult>>? selector = null) 
            where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            // Aplica filtro si se envía
            if (filter != null)
                query = query.Where(filter);

            // Aplica selector si se envía
            if (selector != null)
                return await query.Select(selector).ToListAsync();

            // Si no hay selector, retorna todo el objeto
            return await query.Cast<TResult>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<TResult> GetByIdAsyncWithFilters<TEntity, TResult>(
            Guid id, 
            Expression<Func<TEntity, bool>>? filter = null, 
            Expression<Func<TEntity, TResult>>? selector = null) 
            where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            // Busca la entidad por ID
            query = query.Where(e => EF.Property<Guid>(e, "Id") == id);
            // Si no se encuentra, lanza una excepción
            // Aplica filtro adicional si se envía
            if (filter != null)
                query = query.Where(filter);

            // Aplica selector si se envía
            if (selector != null)
                return await query.Select(selector).FirstOrDefaultAsync();


            if (typeof(TResult) == typeof(TEntity))
                return (TResult?)(object?)await query.FirstOrDefaultAsync();

            throw new InvalidOperationException("TResult must match TEntity when no selector is provided, or provide a selector");
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
