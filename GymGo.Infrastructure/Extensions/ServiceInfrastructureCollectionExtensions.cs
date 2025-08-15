using GymGo.Application.Contracts.Infrastructure;
using GymGo.Infrastructure.DataSeeding;
using GymGo.Infrastructure.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymGo.Infrastructure.Extensions
{
    public static class ServiceInfrastructureCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFileLoggerService, FileLoggerService>();
            services.AddScoped<ICsvDataSeeder, CsvDataSeeder>();
            // ... otros servicios
            return services;
        }
    }
}
