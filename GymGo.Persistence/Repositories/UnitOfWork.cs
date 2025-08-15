using GymGo.Application.Contracts.Persistence;
using GymGo.Domain.Aggregates;
using GymGo.Domain.Common;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Helpers.SqlFileLoader;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymGo.Persistence.Repositories
{
    public class UnitOfWork
        : IUnitOfWork, IDisposable
    {

        //private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ISqlFileLoader _sqlFileLoader;
        private IDbContextTransaction? _currentTransaction;
        private bool _disposed = false;

        public UnitOfWork(
            ApplicationDbContext dbContext,
           // IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IMediator mediator, 
            ISqlFileLoader sqlFileLoader
            )
        {
            _dbContext = dbContext;
            _mediator = mediator;
            //_dbContextFactory = dbContextFactory;
            _sqlFileLoader = sqlFileLoader;
        }

        public System.Data.IDbConnection Connection => _dbContext.Database.GetDbConnection();



        private IClientRepositoryAsync? _clientRepositoryAsync;
        public IClientRepositoryAsync ClientRepositoryAsync =>
            _clientRepositoryAsync ??= new ClientRepositoryAsync(_dbContext);

        private IMembershipTypeRepositoryAsync? _membershipTypeRepositoryAsync;
        public IMembershipTypeRepositoryAsync MembershipTypeRepositoryAsync =>
            _membershipTypeRepositoryAsync ??= new MembershipTypeRepositoryAsync(_dbContext);
  

        private IMembershipTypeUnitOfWork? _membershipTypeUnitOfWork;
        public IMembershipTypeUnitOfWork MembershipTypeUnitOfWork =>
            _membershipTypeUnitOfWork ??= new MembershipTypeUnitOfWork(_dbContext, Connection, _sqlFileLoader);




        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);

            var domainEntities = _dbContext.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAggregateRoot aggregate && aggregate.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => ((IAggregateRoot)e.Entity).DomainEvents)
                .ToList();

            domainEntities.ForEach(e => ((IAggregateRoot)e.Entity).ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // No llamar de nuevo al finalizer
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _currentTransaction?.Dispose();
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public Task Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
            return Task.CompletedTask;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await CommitAsync(cancellationToken);
                await _currentTransaction?.CommitAsync(cancellationToken)!;
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Attach(entity);
        }
    }
}
