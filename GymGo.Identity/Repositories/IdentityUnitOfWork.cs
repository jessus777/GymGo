using GymGo.Application.Contracts.Identity;
using GymGo.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Identity.Repositories
{
    public class IdentityUnitOfWork
        : IIdentityUnitOfWork, IDisposable
    {
        private readonly IdentityDbContext _context;
        private bool _disposed = false;

        public IdentityUnitOfWork(IdentityDbContext context)
        {
            _context = context;
        }

        private IUserRepositoryAsync? _userRepositoryAsync;
        public IUserRepositoryAsync UserRepositoryAsync => 
            _userRepositoryAsync ??= new UserRepositoryAsync(_context);

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // No llamar de nuevo al finalizer        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //_currentTransaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
