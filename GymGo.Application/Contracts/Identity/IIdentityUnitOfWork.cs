using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Application.Contracts.Identity
{
    public interface IIdentityUnitOfWork
        : IDisposable
    {
        IUserRepositoryAsync UserRepositoryAsync { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
