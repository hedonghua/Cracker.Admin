using Cracker.Admin.Extensions;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin
{
    public class MultiTenantDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : AbpDbContext<TDbContext>, IEfCoreDbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TenantDomainService tenantDomainService;

        public MultiTenantDbContextProvider(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, TenantDomainService tenantDomainService)
        {
            this.serviceProvider = serviceProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.tenantDomainService = tenantDomainService;
        }

        public TDbContext GetDbContext()
        {
            var context = (TDbContext)serviceProvider.GetService(typeof(TDbContext))!;

            if (MultiTenancyConsts.IsEnabled)
            {
                var tenantId = GetTenantId();
                if (!string.IsNullOrEmpty(tenantId))
                {
                    var connectionString = TenantExtension.GetConnectionString(Guid.Parse(tenantId!));
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        context.Database.SetConnectionString(connectionString);
                    }
                }
            }

            return context;
        }

        public async Task<TDbContext> GetDbContextAsync()
        {
            var context = (TDbContext)serviceProvider.GetRequiredService(typeof(TDbContext))!;

            if (MultiTenancyConsts.IsEnabled)
            {
                var tenantId = GetTenantId();
                if (!string.IsNullOrEmpty(tenantId))
                {
                    var connectionString = (await tenantDomainService.GetTenantConfigurationsAsync()).FirstOrDefault(x => x.Id == Guid.Parse(tenantId))?.ConnectionStrings?["MySql"];
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        context.Database.SetConnectionString(connectionString);
                    }
                }
            }

            return context;
        }

        private string? GetTenantId()
        {
            return (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant", out var tenantId) ?? false) ? tenantId : default;
        }
    }
}