using Cracker.Admin.Core;
using StackExchange.Redis;
using System.Text.Json;

namespace Cracker.Admin.Infrastructure.Cache
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase _database;

        public RedisCacheProvider(IConnectionMultiplexer redisConnection)
        {
            _database = redisConnection.GetDatabase();
        }

        public async Task<TValue?> GetAsync<TValue>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNull)
            {
                return default;
            }
            return JsonSerializer.Deserialize<TValue?>(value!);
        }

        public async Task SetAsync<TValue>(string key, TValue value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
        {
            var redisValue = JsonSerializer.Serialize(value);
            if (slidingExpiration.HasValue)
            {
                await _database.StringSetAsync(key, redisValue, slidingExpiration.Value);
            }
            else if (absoluteExpiration.HasValue)
            {
                await _database.StringSetAsync(key, redisValue, absoluteExpiration.Value);
            }
            else
            {
                await _database.StringSetAsync(key, redisValue);
            }
        }

        public Task DelAsync(string key)
        {
            return _database.KeyDeleteAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public Task HSetAsync<T>(string key, string field, T value)
        {
            return _database.HashSetAsync(key, field, JsonSerializer.Serialize(value));
        }

        public Task HDelAsync(string key, string field)
        {
            return _database.HashDeleteAsync(key, field);
        }

        public async Task<T?> HGetAsync<T>(string key, string field)
        {
            var value = await  _database.HashGetAsync(key, field);
            return JsonSerializer.Deserialize<T>(value);
        }

        public Task IncrByAsync(string key, int value)
        {
            return _database.StringIncrementAsync(key, value);
        }
    }
}