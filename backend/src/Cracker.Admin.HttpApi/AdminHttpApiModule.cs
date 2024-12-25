using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Cracker.Admin.Core;

using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Cracker.Admin;

[DependsOn(
    typeof(AdminApplicationContractsModule)
    )]
public class AdminHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();

        context.Services.AddTransient<IReHeader>(sp =>
        {
            return sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Features.Get<ReHeader>() ?? ReHeader.Default();
        });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
        });
    }
}