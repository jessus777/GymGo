using GymGo.Application.Contracts.Identity;
using GymGo.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace GymGo.Shared.Extensions
{
    public static class ServiceInfrastructureCollectionExtensions
    {
        public static IServiceCollection AddInConfigurePersistenceServices(this IServiceCollection services)
        {
            //services.AddSingleton<AuditableEntitySaveChangesInterceptor>();

            //services.AddPooledDbContextFactory<ApplicationDbContext>((sp, options) =>
            //{
            //    var interceptor = sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
            //    var configuration = sp.GetRequiredService<IConfiguration>();

            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //    options.AddInterceptors(interceptor);
            //});



            
            
        }
    }
}
