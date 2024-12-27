using Cracker.Admin.Core;
using Cracker.Admin.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.Modularity;

namespace Cracker.Admin.Infrastructure
{
    [DependsOn(
        typeof(CrackerAdminEntityFrameworkCoreModule)
        )]
    public class CrackerAdminInfrastructureModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            var configuration = context.Services.GetConfiguration();
            var isMemory = configuration["CacheProvider:Type"] == "Memory";

            context.Services.AddSingleton<ICacheProvider>(sp =>
            {
                if (isMemory) return new MemoryCacheProvider(sp.GetRequiredService<IMemoryCache>());
                return new RedisCacheProvider(ConnectionMultiplexer.Connect(configuration["CacheProvider:Redis"]!)); 
            });
        }
    }
}