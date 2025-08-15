using Serilog;
using Serilog.Events;

namespace GymGo.Api.Extensions
{
    public static class AddLoggerService
    {
        public static void AddFileLogger(this ConfigureHostBuilder hostBuilder, IConfiguration configuration)
        {
            var filePath = configuration["LogOptions:FilePath"] ?? "Logs/log-.txt";
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("La ruta del archivo de log no está definida correctamente en appsettings.");

            hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.File(
                        path: filePath,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 10,
                        restrictedToMinimumLevel: LogEventLevel.Information);
            });
        }
    }
}
