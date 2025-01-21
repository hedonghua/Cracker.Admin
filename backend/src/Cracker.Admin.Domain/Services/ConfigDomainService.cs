using Cracker.Admin.Entities;
using StackExchange.Redis;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Cracker.Admin.Services
{
    public class ConfigDomainService : DomainService
    {
        private readonly IRepository<SysConfig> repository;
        private readonly IDatabase database;
        private const string ConfigKey = "SysConfig";

        public ConfigDomainService(IRepository<SysConfig> repository, IDatabase database)
        {
            this.repository = repository;
            this.database = database;
        }

        public async Task<string?> GetAsync(string key)
        {
            if (await database.HashExistsAsync(ConfigKey, key))
            {
                return await database.HashGetAsync(ConfigKey, key);
            }

            var value = (await repository.FirstOrDefaultAsync(x => x.Key == key))?.Value;
            if (string.IsNullOrEmpty(value)) return default;

            await database.HashSetAsync(ConfigKey, key, value);
            return value;
        }

        public async Task<FrozenDictionary<string, string>> GetKeysAsync(string groupKey)
        {
            var key = $"{ConfigKey}:{groupKey}";
            if (await database.KeyExistsAsync(key))
            {
                var dict = new Dictionary<string, string>();
                foreach (var item in await database.HashGetAllAsync(ConfigKey))
                {
                    dict.Add(item.Name!, item.Value!);
                }
                return dict.ToFrozenDictionary();
            }

            var map = (await repository.GetQueryableAsync()).Where(x => x.GroupKey == groupKey).ToFrozenDictionary(k => k.Key, v => v.Value);
            foreach (var item in map)
            {
                await database.HashSetAsync(key, item.Key, item.Value);
            } 
            return map;
        }

        public async Task RemoveAsync(string key, string? groupKey = default)
        {
            if (await database.HashExistsAsync(ConfigKey, key))
            {
                await database.HashDeleteAsync(ConfigKey, key);
            }
            if (!string.IsNullOrEmpty(groupKey))
            {
                var cacheKey = $"{ConfigKey}:{groupKey}";
                if (await database.KeyExistsAsync(key))
                {
                    await database.HashDeleteAsync(cacheKey, key);
                }
            }
        }
    }
}