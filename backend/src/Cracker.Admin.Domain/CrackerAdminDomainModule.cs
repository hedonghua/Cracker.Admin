using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.MultiTenancy;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

        context.Services.AddTransient<IKeySettings, GlobalKeySettingsService>();
        context.Services.AddSingleton(imp => FileLoggerService.Instance);
        context.Services.AddConnections();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(Convert.ToInt32(conf.GetSection("JWT")["ClockSkew"])),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = conf.GetSection("JWT")["ValidAudience"],
                    ValidIssuer = conf.GetSection("JWT")["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf.GetSection("JWT")["IssuerSigningKey"]!))
                };
            });
    }
}