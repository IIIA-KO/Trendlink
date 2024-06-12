namespace Trendlink.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);

        Task Remove(string key, CancellationToken cancellationToken = default);
    }
}
