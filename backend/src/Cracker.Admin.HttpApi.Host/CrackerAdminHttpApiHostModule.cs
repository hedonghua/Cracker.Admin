using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

using Cracker.Admin.EntityFrameworkCore;
using Cracker.Admin.Filters;
using Cracker.Admin.Jobs;
using Cracker.Admin.Middlewares;
using Cracker.Admin.MultiTenancy;
using Cracker.Admin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Cracker.Admin.Core;
using Microsoft.AspNetCore.Http;

namespace Cracker.Admin;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(CrackerAdminApplicationModule),
    typeof(CrackerAdminEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
)]
public class CrackerAdminHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        AppManager.BeforeSet(configuration, hostingEnvironment.WebRootPath);

        ConfigureAuthentication(context);
        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
        ConfigureFilters(context, configuration);
        context.Services.AddScheduler();

        context.Services.AddTransient<IReHeader>(sp =>
        {
            return sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Features.Get<ReHeader>() ?? ReHeader.Default();
        });
    }

    private void ConfigureFilters(ServiceConfigurationContext context, IConfiguration configuration)
    {
        Configure<MvcOptions>(options =>
        {
            List<int> indexes = [];
            int i = 0;
            foreach (var filter in options.Filters)
            {
                if (filter is ServiceFilterAttribute attr && attr.ServiceType.Equals(typeof(AbpExceptionFilter)))
                {
                    indexes.Add(i);
                }
                else if (filter.GetType().Name == "AbpAutoValidateAntiforgeryTokenAttribute")
                {
                    indexes.Add(i);
                }
                i++;
            }

            indexes.ForEach(x =>
            {
                options.Filters.RemoveAt(x);
            });

            options.Filters.Add<AppGlobalActionFilter>();
            options.Filters.Add<AppGlobalExceptionFilter>();
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureBundles()
    {
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CrackerAdminDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Cracker.Admin.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<CrackerAdminDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Cracker.Admin.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<CrackerAdminApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Cracker.Admin.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<CrackerAdminApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Cracker.Admin.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            //options.ConventionalControllers.Create(typeof(AdminApplicationModule).Assembly);
        });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "Bearer xxx",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? Array.Empty<string>())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            //app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.UseMiddleware<PermissionMiddleware>();
        app.UseMiddleware<ReHeaderMiddleware>();

        app.UseSwagger();
        app.UseAbpSwaggerUI();

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();

        AppManager.AfterSet(app.ApplicationServices, env.IsDevelopment());

        GlobalKeySettingsService.Instance.InitializationAsync().Wait();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapMqtt("/mqtt");
        });

        app.UseMqttServer(server =>
        {
            server.StartedAsync += args =>
            {
                _ = Task.Run(async () =>
                {
                    var mqttApplicationMessage = new MqttApplicationMessageBuilder()
                        .WithPayload($"Test application message from MQTTnet server.")
                        .WithTopic("message")
                        .Build();

                    while (true)
                    {
                        try
                        {
                            await server.InjectApplicationMessage(new InjectedMqttApplicationMessage(mqttApplicationMessage)
                            {
                                SenderClientId = "server"
                            });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        finally
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5));
                        }
                    }
                });

                return Task.CompletedTask;
            };
        });

        app.ApplicationServices.UseScheduler(sch =>
        {
            //sch.Schedule<Bu>().EverySeconds(5);
        });
    }
}