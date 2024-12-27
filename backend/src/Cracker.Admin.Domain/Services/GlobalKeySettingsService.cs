using Microsoft.Extensions.DependencyInjection;
using Cracker.Admin.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Cracker.Admin.Entities;

namespace Cracker.Admin.Services
{
    public class GlobalKeySettingsService : IKeySettings
    {
        private readonly IRepository<SysDict> _dictRepository;
        private static int _num = 0;

        public GlobalKeySettingsService(IRepository<SysDict> dictRepository)
        {
            _dictRepository = dictRepository;
        }

        public static IKeySettings Instance => _lazyValue.Value;

        private static Lazy<IKeySettings> _lazyValue => new(() =>
        {
            return AppManager.ServiceProvider.GetService<IKeySettings>()!;
        });

        public async Task InitializationAsync(bool refresh = false)
        {
            if (!refresh && _num > 0) throw new Exception("重复初始化");
            var all = await _dictRepository.GetListAsync(x => x.IsEnabled);
            foreach (var item in all)
            {
                await RedisHelper.HSetAsync(AdminConsts.DictCacheHashKey, item.Key, item.Value);
            }
            Interlocked.Increment(ref _num);
        }

        public async Task<string> GetAsync(string key)
        {
            return await RedisHelper.HGetAsync(AdminConsts.DictCacheHashKey, key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await RedisHelper.HDelAsync(AdminConsts.DictCacheHashKey, [key]) > 0;
        }

        public async Task<bool> SetAsync(string key, string value, bool isDbSync = false)
        {
            var isSuccess = await RedisHelper.HSetAsync(AdminConsts.DictCacheHashKey, key, value);
            if (isSuccess && isDbSync)
            {
                var entity = await _dictRepository.SingleOrDefaultAsync(x => x.Key.ToLower() == key.ToLower());
                if (entity != null)
                {
                    entity.Value = value;
                    await _dictRepository.UpdateAsync(entity, true);
                }
            }
            return true;
        }
    }
}