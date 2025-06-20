namespace App.Application.Interfaces.Logging
{
    public interface ILogService
    {
        void LogError(string message, Exception exception=null);
        void LogWarning(string message);
        void LogInformation(string message);
    }
}
