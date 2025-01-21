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
//        private readonly IDatabase redisDb;
//        private static int _num = 0;

//        public GlobalKeySettingsService(IRepository<SysDict> dictRepository, IDatabase redisDb)
//        {
//            _dictRepository = dictRepository;
//            this.redisDb = redisDb;
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
//                await redisDb.HSetAsync(AdminConsts.DictCacheHashKey, item.Key, item.Value);
//            }
//            Interlocked.Increment(ref _num);
//        }

//        public async Task<string> GetAsync(string key)
//        {
//            return await redisDb.HGetAsync<string>(AdminConsts.DictCacheHashKey, key);
//        }

//        public async Task<bool> RemoveAsync(string key)
//        {
//            await redisDb.HDelAsync(AdminConsts.DictCacheHashKey, key);
//            return true;
//        }

//        public async Task<bool> SetAsync(string key, string value, bool isDbSync = false)
//        {
//            await redisDb.HSetAsync(AdminConsts.DictCacheHashKey, key, value);
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