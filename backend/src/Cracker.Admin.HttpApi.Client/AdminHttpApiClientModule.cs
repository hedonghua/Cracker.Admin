using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Cracker.Admin;

[DependsOn(
    typeof(AdminApplicationContractsModule)
)]
public class AdminHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //context.Services.AddHttpClientProxies(
        //    typeof(AdminApplicationContractsModule).Assembly,
        //    RemoteServiceName
        //);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdminHttpApiClientModule>();
        });
    }
}