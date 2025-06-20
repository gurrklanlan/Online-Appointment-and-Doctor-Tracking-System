using App.Application.Interfaces.Logging;

namespace App.Logging
{
    public class LogService : ILogService
    {
        private readonly ILogService _logService;
        public LogService(ILogService logService)
        {
            _logService = logService;
        }

        public void LogError(string message, Exception exception = null)
        {
           _logService.LogError(message, exception);
        }

        public void LogInformation(string message)
        {
            _logService.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logService.LogWarning(message);
        }
    }
}
