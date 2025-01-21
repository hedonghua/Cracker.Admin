using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.Modularity;

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

            context.Services.AddSingleton(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["Redis:Connection"]!).GetDatabase();
            });
        }
    }
}