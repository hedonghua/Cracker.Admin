//using Cracker.Admin.Core;
//using Cracker.Admin.Entities;
//using Microsoft.Extensions.DependencyInjection;
//using System.Threading;
//using System.Threading.Tasks;
//using Volo.Abp.Domain.Repositories;

//namespace Cracker.Admin.Services
//{
//    public class GlobalKeySettingsService : IKeySettings
//    {
//        private readonly IRepository<SysDict> _dictRepository;
//        private readonly ICacheProvider cacheProvider;
//        private static int _num = 0;

//        public GlobalKeySettingsService(IRepository<SysDict> dictRepository, ICacheProvider cacheProvider)
//        {
//            _dictRepository = dictRepository;
//            this.cacheProvider = cacheProvider;
//        }

//        public static IKeySettings Instance => _lazyValue.Value;

//        private static Lazy<IKeySettings> _lazyValue => new(() =>
//        {
//            return AppManager.ServiceProvider.GetService<IKeySettings>()!;
//        });

//        public async Task InitializationAsync(bool refresh = false)
//        {
//            if (!refresh && _num > 0) throw new Exception("重复初始化");
//            var all = await _dictRepository.GetListAsync(x => x.IsEnabled);
//            foreach (var item in all)
//            {
//                await cacheProvider.HSetAsync(AdminConsts.DictCacheHashKey, item.Key, item.Value);
//            }
//            Interlocked.Increment(ref _num);
//        }

//        public async Task<string> GetAsync(string key)
//        {
//            return await cacheProvider.HGetAsync<string>(AdminConsts.DictCacheHashKey, key);
//        }

//        public async Task<bool> RemoveAsync(string key)
//        {
//            await cacheProvider.HDelAsync(AdminConsts.DictCacheHashKey, key);
//            return true;
//        }

//        public async Task<bool> SetAsync(string key, string value, bool isDbSync = false)
//        {
//            await cacheProvider.HSetAsync(AdminConsts.DictCacheHashKey, key, value);
//            if (isDbSync)
//            {
//                var entity = await _dictRepository.SingleOrDefaultAsync(x => x.Key.ToLower() == key.ToLower());
//                if (entity != null)
//                {
//                    entity.Value = value;
//                    await _dictRepository.UpdateAsync(entity, true);
//                }
//            }
//            return true;
//        }
//    }
//}