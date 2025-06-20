namespace App.Application.Interfaces.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);

        Task AddAsync<T>(string key, T value, TimeSpan timeSpan);
        Task RemoveAsync(string key);

    }
}
