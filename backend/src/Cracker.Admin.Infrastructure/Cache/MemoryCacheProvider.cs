using Cracker.Admin.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Cracker.Admin.Infrastructure.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public Task<TValue?> GetAsync<TValue>(string key)
        {
            _memoryCache.TryGetValue(key, out TValue? value);
            return Task.FromResult(value);
        }

        public Task SetAsync<TValue>(string key, TValue value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            if (slidingExpiration.HasValue)
            {
                cacheEntryOptions.SetSlidingExpiration(slidingExpiration.Value);
            }
            if (absoluteExpiration.HasValue)
            {
                cacheEntryOptions.SetAbsoluteExpiration(absoluteExpiration.Value);
            }

            _memoryCache.Set(key, value, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public Task DelAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string key)
        {
            var exist = _memoryCache.TryGetValue(key, out _);
            return Task.FromResult(exist);
        }

        public Task HSetAsync<T>(string key, string field, T value)
        {
            throw new NotImplementedException();
        }

        public Task HDelAsync(string key, string field)
        {
            throw new NotImplementedException();
        }

        public Task<T?> HGetAsync<T>(string key, string field)
        {
            throw new NotImplementedException();
        }

        public Task IncrByAsync(string key, int value)
        {
            throw new NotImplementedException();
        }
    }
}