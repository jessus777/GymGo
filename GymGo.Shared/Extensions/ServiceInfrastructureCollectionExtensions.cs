using GymGo.Application.Contracts;
using GymGo.Application.Contracts.Identity;
using GymGo.Application.Contracts.Persistence;
using GymGo.Identity.Contexts;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymGo.Shared.Extensions
{
    public static class ServiceInfrastructureCollectionExtensions
    {
        public static IServiceCollection AddInConfigurePersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<AuditableEntitySaveChangesInterceptor>();

            services.AddPooledDbContextFactory<ApplicationDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
                var configuration = sp.GetRequiredService<IConfiguration>();

                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(interceptor);
            });

            services.AddDbContextFactory<IdentityDbContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"));
            }, ServiceLifetime.Scoped); // <- clave

            services.AddScoped<IUnitOfWorkFactory, AppUnitOfWorkFactory>();
            services.AddScoped<IIdentityUnitOfWork>(sp => sp.GetRequiredService<IUnitOfWorkFactory>().CreateIdentity());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IUnitOfWorkFactory>().Create());

            return services;
        }
    }
}
