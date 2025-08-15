namespace GymGo.Application.Contracts.Identity
{
    public interface IIdentityUnitOfWork
        : IDisposable
    {
        IUserRepositoryAsync UserRepositoryAsync { get; }
        IRoleRepositoryAsync RoleRepositoryAsync { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
