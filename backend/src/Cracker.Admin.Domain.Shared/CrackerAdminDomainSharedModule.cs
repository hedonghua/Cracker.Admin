using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Cracker.Admin;

[DependsOn(
        typeof(AbpDddDomainSharedModule)
    )]
public class CrackerAdminDomainSharedModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var conf = context.Services.GetConfiguration();

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CrackerAdminDomainSharedModule>();
        });

        var csRedis = new CSRedis.CSRedisClient(conf["Redis:Connection"]);
        RedisHelper.Initialization(csRedis);
    }
}