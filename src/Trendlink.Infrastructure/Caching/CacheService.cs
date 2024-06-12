﻿using Microsoft.Extensions.Caching.Distributed;
using System.Buffers;
using System.Text.Json;
using Trendlink.Application.Abstractions.Caching;

namespace Trendlink.Infrastructure.Caching
{
    internal sealed class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            this._cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            byte[]? bytes = await this._cache.GetAsync(key, cancellationToken);

            return bytes is null ? default : Deserialize<T>(bytes);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            byte[] bytes = Serialize(value);

            return this._cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return this._cache.RemoveAsync(key, cancellationToken);
        }

        private static T Deserialize<T>(byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes)!;
        }

        private static byte[] Serialize<T>(T value)
        {
            var buffer = new ArrayBufferWriter<byte>();

            using var writer = new Utf8JsonWriter(buffer);

            JsonSerializer.Serialize(writer, value);

            return buffer.WrittenSpan.ToArray();
        }

    }
}
