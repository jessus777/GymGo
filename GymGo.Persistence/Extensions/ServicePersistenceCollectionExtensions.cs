using GymGo.Application.Contracts;
using GymGo.Application.Contracts.Infrastructure;
using GymGo.Application.Contracts.Persistence;
using GymGo.Persistence.Contexts;
using GymGo.Persistence.Helpers.SqlFileLoader;
using GymGo.Persistence.Interceptors;
using GymGo.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace GymGo.Persistence.Extensions
{
    public static class ServicePersistenceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            //services.AddSingleton<AuditableEntitySaveChangesInterceptor>();
            
            //services.AddPooledDbContextFactory<ApplicationDbContext>((sp, options) =>
            //{
            //    var interceptor = sp.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
            //    var configuration = sp.GetRequiredService<IConfiguration>();

            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //    options.AddInterceptors(interceptor);
            //});
 

            services.AddSingleton<ISqlFileLoader>(
                 sp => new SqlFileLoader(Path.Combine(AppContext.BaseDirectory, "SqlQueries"))
             );

            //services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            //services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IUnitOfWorkFactory>().Create());
            //services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            //services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            //services.AddScoped(typeof(IDapperRepositoryAsync<>), typeof(DapperRepositoryAsync<>));

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IMembershipTypeUnitOfWork, MembershipTypeUnitOfWork>();
            //services.AddScoped<IMembershipTypeDapperQueriesRepositoryAsync, MembershipTypeDapperQueriesRepositoryAsync>();
            //services.AddScoped<IMembershipRepositoryAsync, MembershipRepositoryAsync>();
            //services.AddScoped<IMembershipTypeRepositoryAsync, MembershipTypeRepositoryAsync>();
            //services.AddScoped<IClientHistoryRepositoryAsync, ClientHistoryRepositoryAsync>();
            //services.AddScoped<IClientRepositoryAsync, ClientRepositoryAsync>();
            return services;
        }
    }
}
