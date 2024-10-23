using MediatR;
using Microsoft.Extensions.Logging;
using Trendlink.Application.Abstractions.Caching;
using Trendlink.Domain.Abstraction;

namespace Trendlink.Application.Abstractions.Behaviors
{
    internal sealed class QueryCachingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
        where TResponse : Result
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

        public QueryCachingBehavior(
            ICacheService cacheService,
            ILogger<QueryCachingBehavior<TRequest, TResponse>> logger
        )
        {
            this._cacheService = cacheService;
            this._logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            TResponse? cachedResult = await this._cacheService.GetAsync<TResponse>(
                request.CacheKey,
                cancellationToken
            );

            string name = typeof(TRequest).Name;

            if (cachedResult is not null)
            {
                this._logger.LogInformation("Cache hit for {Query}", name);

                return cachedResult;
            }

            this._logger.LogInformation("Cache miss for {Query}", name);

            TResponse result = await next();

            if (result.IsSuccess)
            {
                await this._cacheService.SetAsync(
                    request.CacheKey,
                    result,
                    request.Expiration,
                    cancellationToken
                );
            }

            return result;
        }
    }
}
