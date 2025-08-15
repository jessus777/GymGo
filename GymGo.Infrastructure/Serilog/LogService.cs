using GymGo.Application.Contracts.Infrastructure;
using Serilog;

namespace GymGo.Infrastructure.Serilog
{
    public class LogService
        : ILogService
    {
        public void Error(string message, Exception ex = null)
        {
            if (ex == null)
                Log.Error(message);
            else
                Log.Error(ex, message);
        }

        public void Info(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }
    }
}
