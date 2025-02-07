using Cracker.Admin.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

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

            context.Services.AddTransient(sp =>
            {
                var tenant = sp.GetRequiredService<ICurrentTenant>();
                if (tenant != null && !string.IsNullOrEmpty(tenant.Name))
                {
                    var connection = TenantExtension.GetRedisConnection(tenant.Name);
                    if (string.IsNullOrEmpty(connection))
                    {
                        throw new HostAbortedException("租户配置错误，Redis连接字符串为空");
                    }
                    return ConnectionMultiplexer.Connect(connection).GetDatabase();
                }
                return ConnectionMultiplexer.Connect(configuration["Redis:Connection"]!).GetDatabase(0);
            });
        }
    }
}