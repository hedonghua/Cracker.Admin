using Cracker.Admin.Core;
using Cracker.Admin.MultiTenancy;
using Cracker.Admin.Services;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin;

[DependsOn(
    typeof(CrackerAdminDomainSharedModule),
    typeof(AbpDddDomainModule)
)]
public class CrackerAdminDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var conf = context.Services.GetConfiguration();

        //context.Services.AddTransient<IKeySettings, GlobalKeySettingsService>();
        //context.Services.AddConnections();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
    }
}