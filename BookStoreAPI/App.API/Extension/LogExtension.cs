using System.Runtime.CompilerServices;
using App.Application.Interfaces.Logging;
using App.Logging;
using Serilog;

namespace App.API.Extension
{
    public static class LogExtension
    {
        public static IServiceCollection AddLogExt(this IServiceCollection services)
        {
           

            services.AddScoped<ILogService, LogService>();
            
            return services;
        }
    }
}
