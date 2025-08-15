using GymGo.Application.Contracts.Infrastructure;
using GymGo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GymGo.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor
        : SaveChangesInterceptor
    {
        private readonly IFileLoggerService _fileLoggerService;
        //falta agregar el servicio para obtener el usuario actual

        public AuditableEntitySaveChangesInterceptor(IFileLoggerService fileLoggerService)
        {
            _fileLoggerService = fileLoggerService;
        }

       
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default
            )
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditFields(DbContext? context)
        {
            if (context == null) return;

            var now = DateTime.UtcNow;
            //var userId = _currentUserService.UserId ?? "system";
            var userId =  "system";

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = now;
                    entry.Entity.CreatedBy = userId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDate = now;
                    entry.Entity.LastModifiedBy = userId;
                }
            }
        }
    }
}
