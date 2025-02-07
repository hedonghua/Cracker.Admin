using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Middlewares
{
    public class MultiTenancyMiddleware : IMiddleware
    {
        private readonly IRepository<SysTenant> tenantRepository;

        public MultiTenancyMiddleware(IRepository<SysTenant> tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var hasTenantId = context.Request.Headers.TryGetValue("X-Tenant", out var tenantId);
            if (hasTenantId && !string.IsNullOrEmpty(tenantId.ToString()))
            {
                var tenant = await tenantRepository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(tenantId!));
                if (tenant != null)
                {
                    TenantExtension.Tenants.TryAdd(tenant.Name!, new TenantConfiguration
                    {
                        Id = tenant.Id,
                        Name = tenant.Name,
                        ConnectionStrings = new ConnectionStrings
                        {
                            ["MySql"] = tenant.ConnectionString,
                            ["Redis"] = tenant.RedisConnection,
                        }
                    });
                    context.Features.Set(new TenantAccessorImpl
                    {
                        Current = new BasicTenantInfo(tenant.Id, tenant.Name)
                    });
                }
            }

            await next(context);
        }
    }
}