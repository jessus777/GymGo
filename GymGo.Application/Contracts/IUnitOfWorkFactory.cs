using GymGo.Application.Contracts.Identity;
using GymGo.Application.Contracts.Persistence;

namespace GymGo.Application.Contracts
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        IIdentityUnitOfWork CreateIdentity();
    }
}
