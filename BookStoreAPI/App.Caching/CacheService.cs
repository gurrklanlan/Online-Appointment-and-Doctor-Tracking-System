using App.Application.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task AddAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeSpan
            };
            memoryCache.Set(key, value, cacheOptions);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            if(memoryCache.TryGetValue(key, out T cacheItem)) return Task.FromResult(cacheItem);
            {
                return Task.FromResult(default(T));
            }
        }

        public Task RemoveAsync(string key)
        {
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
