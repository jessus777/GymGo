using GymGo.Application.Contracts.Identity;

namespace GymGo.Application.Contracts.Persistence
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
