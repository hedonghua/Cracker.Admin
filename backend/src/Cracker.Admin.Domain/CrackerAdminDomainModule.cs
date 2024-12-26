using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

using MQTTnet.AspNetCore;

using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.MultiTenancy;
using Cracker.Admin.Services;

using Volo.Abp;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Domain;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin;

[DependsOn(
    typeof(CrackerAdminDomainSharedModule),
    typeof(AbpEmailingModule),
    typeof(AbpDddDomainModule),
    typeof(AbpBlobStoringFileSystemModule),
    typeof(AbpBackgroundJobsModule)
)]
public class CrackerAdminDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var conf = context.Services.GetConfiguration();

        context.Services.AddTransient<IKeySettings, GlobalKeySettingsService>();
        context.Services.AddSingleton(imp => FileLoggerService.Instance);
        //context.Services.AddHostedService<MqttServerHostService>();
        context.Services.AddMqttServer(conf =>
        {
            conf.WithDefaultEndpointPort(10086);
        });
        context.Services.AddConnections();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif

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

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = conf["App:FileSystemStoragePath"]!;
                });
            });
        });

        context.Services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(ReflectionHelper.GetMyAssemblies());
        });
    }
}