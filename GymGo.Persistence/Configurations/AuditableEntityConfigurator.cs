using GymGo.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Persistence.Configurations
{
    public static class AuditableEntityConfigurator
    {
        public static void ConfigureAuditableEntities(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    var builder = modelBuilder.Entity(entityType.ClrType);
                    var navigation = entityType.FindNavigation(nameof(IAuditable.Auditoria));
                    if (navigation != null)
                    {
                        builder.OwnsOne(typeof(AuditableEntity), nameof(IAuditable.Auditoria), audit =>
                        {
                            audit.Property(nameof(AuditableEntity.CreatedBy)).HasColumnName("CreatedBy");
                            audit.Property(nameof(AuditableEntity.CreatedDate)).HasColumnName("CreatedDate");
                            audit.Property(nameof(AuditableEntity.LastModifiedBy)).HasColumnName("LastModifiedBy");
                            audit.Property(nameof(AuditableEntity.LastModifiedDate)).HasColumnName("LastModifiedDate");
                        });
                    }
                }
            }
        }
    }
}
