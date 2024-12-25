using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Cracker.Admin;

[DependsOn(
    typeof(AdminDomainSharedModule),
    typeof(AbpObjectExtendingModule),
    typeof(AbpDddApplicationContractsModule)
)]
public class AdminApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminDtoExtensions.Configure();
    }
}