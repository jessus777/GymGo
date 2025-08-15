using GymGo.Application.Contracts.Infrastructure;
using Serilog;

namespace GymGo.Infrastructure.Serilog
{
    public class FileLoggerService
        : IFileLoggerService
    {
        public void LogError(string message, Exception ex)
        {
            Log.ForContext("Destination", "File").Error(ex, message);
        }

        public void LogInformation(string message)
        {
            Log.ForContext("Destination", "File").Information(message);
        }

        public void Warning(string message)
        {
            Log.ForContext("Destination", "File").Warning(message);
        }
    }
}
