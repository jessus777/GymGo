namespace GymGo.Application.Contracts.Identity
{
    public interface IUnitOfWorkIdentityFactory
    {
        IIdentityUnitOfWork Create();
    }
}
