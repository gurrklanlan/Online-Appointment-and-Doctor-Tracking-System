using App.Domain.ExceptionHandler;
using Microsoft.AspNetCore.Diagnostics;

namespace App.API.ExcepitonHandler
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is CriticalException)
            {
                Console.WriteLine($"Critical exception occurred: {exception.Message}");
            }
            return ValueTask.FromResult(false);
        }
    }
}
