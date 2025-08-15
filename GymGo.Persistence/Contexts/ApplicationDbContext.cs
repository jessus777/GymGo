using GymGo.Domain.Entities;
using GymGo.Persistence.Configurations;
using GymGo.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace GymGo.Persistence.Contexts
{
    public class ApplicationDbContext
        : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options//,
            )
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AuditableEntityConfigurator.ConfigureAuditableEntities(modelBuilder);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientHistory> ClientHistories { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
    }
}
