using System.Data;


namespace GymGo.Application.Contracts.Persistence
{
    public interface IUnitOfWork
        : IDisposable
    {
        IClientRepositoryAsync ClientRepositoryAsync { get; }

        IMembershipTypeUnitOfWork MembershipTypeUnitOfWork { get; }
        IMembershipTypeRepositoryAsync MembershipTypeRepositoryAsync { get; }
        //IMembershipTypeDapperQueriesRepositoryAsync MembershipTypeDapperQueriesRepositoryAsync { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync();

        Task CommitAsync(CancellationToken cancellationToken = default);
        Task Rollback();

        IDbConnection Connection { get; }
        void Attach<TEntity>(TEntity entity) where TEntity : class;
    }
}
