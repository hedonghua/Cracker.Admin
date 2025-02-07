using Cracker.Admin.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin
{
    public class MultiTenantDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : AbpDbContext<TDbContext>, IEfCoreDbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public MultiTenantDbContextProvider(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor,IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public TDbContext GetDbContext()
        {
            var context = (TDbContext)serviceProvider.GetService(typeof(TDbContext))!;

            if (bool.Parse(configuration["App:MultiTenancy"]!))
            {
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
            }

            return context;
        }

        public async Task<TDbContext> GetDbContextAsync()
        {
            return GetDbContext();
        }
    }
}