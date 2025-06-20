namespace App.Domain.ExceptionHandler;

    public class CriticalException(string message) : Exception(message);
  