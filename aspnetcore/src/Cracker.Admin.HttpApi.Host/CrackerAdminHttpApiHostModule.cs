using Coravel;
using Cracker.Admin.Core;
using Cracker.Admin.Filters;
using Cracker.Admin.Helpers;
using Cracker.Admin.Infrastructure;
using Cracker.Admin.Jobs;
using Cracker.Admin.JsonConverters;
using Cracker.Admin.Middlewares;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MQTTnet.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;

namespace Cracker.Admin;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CrackerAdminApplicationModule),
    typeof(CrackerAdminInfrastructureModule),
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

        ConfigureAuthentication(context, configuration);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
        ConfigureFilters(context, configuration);
        ConfigureApiBehavior(context, configuration);

        Configure<AbpJsonOptions>(options =>
        {
            options.OutputDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        });
        Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new StringNullableJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new StringJsonConverter());
        });
        Configure<KestrelServerOptions>(options =>
        {
            options.ListenAnyIP(port: int.Parse(configuration["Mqtt:Port"]!), l => l.UseMqtt());
            options.ListenAnyIP(port: int.Parse(Environment.GetEnvironmentVariable("ASPNETCORE_PORT")!));
        });

        context.Services.AddScheduler();

        context.Services.AddTransient<IReHeader>(sp =>
        {
            return sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Features.Get<ReHeader>() ?? ReHeader.Default();
        });
        context.Services.AddTransient<MultiTenancyMiddleware>();
        context.Services.AddTransient<ICurrentTenantAccessor>(sp =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            return httpContext?.Features.Get<TenantAccessorImpl>() ?? new TenantAccessorImpl();
        });
        context.Services.AddRateLimiter(_ => _
            .AddTokenBucketLimiter(policyName: "token", options =>
            {
                options.TokenLimit = 100;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                options.TokensPerPeriod = 20;
                options.AutoReplenishment = true;
            }));

        context.Services.AddHostedService<DatabaseMigrationHostService>();
        context.Services.AddHostedService<PreparationHostService>();

        context.Services.AddHostedMqttServer(
            optionsBuilder =>
            {
                optionsBuilder.WithDefaultEndpoint();
            });

        context.Services.AddMqttConnectionHandler();
        context.Services.AddConnections();

        SnowflakeHelper.Init(short.Parse(configuration["Snowflake:WorkerId"]!), short.Parse(configuration["Snowflake:DataCenterId"]!));
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

            options.Filters.Add<AppGlobalExceptionFilter>();
            //options.Filters.Add<XssProtectionFilterAttribute>();
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(Convert.ToInt32(configuration.GetSection("Jwt")["ClockSkew"])),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration.GetSection("Jwt")["ValidAudience"],
                    ValidIssuer = configuration.GetSection("Jwt")["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["IssuerSigningKey"]!))
                };
            });
        context.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, IdentityMiddlewareResultHandler>();
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
                        .ToArray() ?? [])
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    private void ConfigureApiBehavior(ServiceConfigurationContext context, IConfiguration configuration)
    {
        Configure<ApiBehaviorOptions>(setupAction =>
        {
            setupAction.InvalidModelStateResponseFactory = context =>
            {
                // 自定义错误格式
                var errors = new Dictionary<string, string[]>();
                foreach (var key in context.ModelState.Keys)
                {
                    if (context.ModelState.TryGetValue(key, out var modelState))
                    {
                        errors[key] = modelState.Errors.Select(e => e.ErrorMessage).ToArray();
                    }
                }

                return new ObjectResult(A.Fail(errors.First().Value.First()))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            };
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var configuration = context.GetConfiguration();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseAbpSwaggerUI();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMiddleware<MultiTenancyMiddleware>();
        }
        //TODO: 生产环境可以去掉
        app.UseMiddleware<DemonstrationModeMiddleware>();
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseRateLimiter();

        app.UseMiddleware<ReHeaderMiddleware>();

        app.UseAuditing();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller}/{action}/{param:regex(.*+)}"
             );
            endpoints.MapControllers().RequireRateLimiting("token");
            endpoints.MapConnectionHandler<MqttConnectionHandler>(
                "/mqtt", httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
                    protocolList => protocolList.FirstOrDefault() ?? string.Empty);
            app.UseMqttServer(
                server =>
                {
                    var mqttService = app.ApplicationServices.GetRequiredService<MqttService>();
                    server.ValidatingConnectionAsync += mqttService.ValidatingConnectionAsync;
                });
        });

        app.ApplicationServices.UseScheduler(sch =>
        {
            sch.Schedule<NotificationJob>().EveryMinute().RunOnceAtStart();
        });
    }
}