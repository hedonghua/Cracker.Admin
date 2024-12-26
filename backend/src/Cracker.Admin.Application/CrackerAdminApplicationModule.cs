using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Cracker.Admin;

[DependsOn(
    typeof(CrackerAdminDomainModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpDddApplicationModule),
    typeof(CrackerAdminApplicationContractsModule)
    )]
public class CrackerAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CrackerAdminApplicationModule>();
        });
    }
}