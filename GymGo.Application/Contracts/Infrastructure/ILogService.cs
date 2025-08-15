namespace GymGo.Application.Contracts.Infrastructure
{
    public interface ILogService
    {
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception ex = null);
    }
}
