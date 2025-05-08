using Cracker.Admin.Core;
using Cracker.Admin.Extensions;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Middlewares
{
    public class MultiTenancyMiddleware : IMiddleware
    {
        private readonly TenantDomainService tenantDomainService;

        public MultiTenancyMiddleware(TenantDomainService tenantDomainService)
        {
            this.tenantDomainService = tenantDomainService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var hasTenantId = context.Request.Headers.TryGetValue("X-Tenant", out var tenantId);
            if (hasTenantId && !string.IsNullOrEmpty(tenantId.ToString()))
            {
                var tenants = await tenantDomainService.GetTenantConfigurationsAsync();
                var tenant = tenants.FirstOrDefault(x => x.Id == Guid.Parse(tenantId!));
                if (tenant != null)
                {
                    TenantExtension.Tenants.TryAdd(tenant.Id!, tenant);
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