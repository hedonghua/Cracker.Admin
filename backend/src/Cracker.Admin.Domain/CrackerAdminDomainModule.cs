using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin;

[DependsOn(
    typeof(CrackerAdminDomainSharedModule),
    typeof(AbpDddDomainModule),
    typeof(AbpBackgroundJobsModule)
)]
public class CrackerAdminDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        //context.Services.AddTransient<IKeySettings, GlobalKeySettingsService>();
        //context.Services.AddConnections();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = bool.Parse(configuration["App:MultiTenancy"]!);
        });
    }
}