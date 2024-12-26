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
    }
}