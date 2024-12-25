using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Cracker.Admin;

[DependsOn(
        typeof(AbpDddDomainSharedModule)
    )]
public class AdminDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminGlobalFeatureConfigurator.Configure();
        AdminModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var conf = context.Services.GetConfiguration();

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminDomainSharedModule>();
        });

        var csRedis = new CSRedis.CSRedisClient(conf["Redis:Connection"]);
        RedisHelper.Initialization(csRedis);
    }
}