using Cracker.Admin.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin
{
    public class MultiTenantDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : AbpDbContext<TDbContext>, IEfCoreDbContext
    {
        private readonly ICurrentTenant currentTenant;
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MultiTenantDbContextProvider(ICurrentTenant currentTenant, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.currentTenant = currentTenant;
            this.serviceProvider = serviceProvider;
            this.httpContextAccessor = httpContextAccessor;
        }

        public TDbContext GetDbContext()
        {
            var context = (TDbContext)serviceProvider.GetService(typeof(TDbContext))!;

            var tenantId = new StringValues();
            var hasTenantId = httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant", out var tenantIdRaw) ?? false;
            if (hasTenantId && !string.IsNullOrEmpty(tenantId))
            {
                var connectionString = TenantExtension.GetConnectionString(Guid.Parse(tenantId!));
                if (!string.IsNullOrEmpty(connectionString))
                {
                    context.Database.SetConnectionString(connectionString);
                }
            }

            return context;
        }

        public async Task<TDbContext> GetDbContextAsync()
        {
            var context = (TDbContext)serviceProvider.GetService(typeof(TDbContext))!;

            var tenantId = new StringValues();
            var hasTenantId = httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant", out tenantId) ?? false;
            if (hasTenantId && !string.IsNullOrEmpty(tenantId))
            {
                var connectionString = TenantExtension.GetConnectionString(Guid.Parse(tenantId!));
                if (!string.IsNullOrEmpty(connectionString))
                {
                    context.Database.SetConnectionString(connectionString);
                }
            }

            return context;
        }
    }
}