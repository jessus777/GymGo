namespace GymGo.Application.Contracts.Infrastructure
{
    public interface IFileLoggerService
    {
        void LogInformation(string message);
        void Warning(string message);
        void LogError(string message, Exception ex);
    }
}
