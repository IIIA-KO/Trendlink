using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Abstractions.Caching
{
    public interface ICachedQuery<TReposnse> : IQuery<TReposnse>, ICachedQuery;

    public interface ICachedQuery
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
