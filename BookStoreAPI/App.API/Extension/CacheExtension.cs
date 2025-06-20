using App.Application.Interfaces.Caching;
using App.Caching;

namespace App.API.Extension
{
    public static class CacheExtension
    {
        public static IServiceCollection AddCacheExt(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddMemoryCache();
            return services;
        }
    }
}
