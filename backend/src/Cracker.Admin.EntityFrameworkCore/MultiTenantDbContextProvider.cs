using Cracker.Admin.Extensions;
using Microsoft.EntityFrameworkCore;
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

        public MultiTenantDbContextProvider(ICurrentTenant currentTenant, IServiceProvider serviceProvider)
        {
            this.currentTenant = currentTenant;
            this.serviceProvider = serviceProvider;
        }

        public TDbContext GetDbContext()
        {
            var context = (TDbContext)serviceProvider.GetService(typeof(TDbContext))!;

            if (!string.IsNullOrEmpty(currentTenant.Name))
            {
                var connectionString = TenantExtension.GetConnectionString(currentTenant.Name);
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

            if (!string.IsNullOrEmpty(currentTenant.Name))
            {
                var connectionString = TenantExtension.GetConnectionString(currentTenant.Name);
                if (!string.IsNullOrEmpty(connectionString))
                {
                    context.Database.SetConnectionString(connectionString);
                }
            }

            return context;
        }
    }
}