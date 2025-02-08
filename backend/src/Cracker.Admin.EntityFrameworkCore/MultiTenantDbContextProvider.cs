using Cracker.Admin.Extensions;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using Volo.Abp.Uow.EntityFrameworkCore;

namespace Cracker.Admin
{
    public class MultiTenantDbContextProvider<TDbContext> : UnitOfWorkDbContextProvider<TDbContext>, IDbContextProvider<TDbContext>
        where TDbContext : AbpDbContext<TDbContext>, IEfCoreDbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TenantDomainService tenantDomainService;

        public MultiTenantDbContextProvider(IUnitOfWorkManager unitOfWorkManager, IConnectionStringResolver connectionStringResolver, ICancellationTokenProvider cancellationTokenProvider, ICurrentTenant currentTenant, IEfCoreDbContextTypeProvider efCoreDbContextTypeProvider
            , IHttpContextAccessor httpContextAccessor, TenantDomainService tenantDomainService) : base(unitOfWorkManager, connectionStringResolver, cancellationTokenProvider, currentTenant, efCoreDbContextTypeProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tenantDomainService = tenantDomainService;
        }

        [Obsolete("Use GetDbContextAsync method.")]
        public override TDbContext GetDbContext()
        {
            if (UnitOfWork.EnableObsoleteDbContextCreationWarning &&
                !Volo.Abp.Uow.UnitOfWorkManager.DisableObsoleteDbContextCreationWarning.Value)
            {
                Logger.LogWarning(
                    "UnitOfWorkDbContextProvider.GetDbContext is deprecated. Use GetDbContextAsync instead! " +
                    "You are probably using LINQ (LINQ extensions) directly on a repository. In this case, use repository.GetQueryableAsync() method " +
                    "to obtain an IQueryable<T> instance and use LINQ (LINQ extensions) on this object. "
                );
                Logger.LogWarning(Environment.StackTrace.Truncate(2048));
            }

            var unitOfWork = UnitOfWorkManager.Current;
            if (unitOfWork == null)
            {
                throw new AbpException("A DbContext can only be created inside a unit of work!");
            }

            var targetDbContextType = EfCoreDbContextTypeProvider.GetDbContextType(typeof(TDbContext));
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName(targetDbContextType);
            var connectionString = ResolveConnectionString(connectionStringName);

            if (MultiTenancyConsts.IsEnabled)
            {
                var tenantId = GetTenantId();
                if (!string.IsNullOrEmpty(tenantId))
                {
                    var tenantConnectionString = TenantExtension.GetConnectionString(Guid.Parse(tenantId));
                    if (!string.IsNullOrEmpty(tenantConnectionString))
                    {
                        connectionString = tenantConnectionString;
                    }
                }
            }

            var dbContextKey = $"{targetDbContextType.FullName}_{connectionString}";

            var databaseApi = unitOfWork.GetOrAddDatabaseApi(
                dbContextKey,
                () => new EfCoreDatabaseApi(
                    CreateDbContext(unitOfWork, connectionStringName, connectionString)
                ));

            return (TDbContext)((EfCoreDatabaseApi)databaseApi).DbContext;
        }

        public override async Task<TDbContext> GetDbContextAsync()
        {
            var unitOfWork = UnitOfWorkManager.Current;
            if (unitOfWork == null)
            {
                throw new AbpException("A DbContext can only be created inside a unit of work!");
            }

            var targetDbContextType = EfCoreDbContextTypeProvider.GetDbContextType(typeof(TDbContext));
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName(targetDbContextType);
            var connectionString = await ResolveConnectionStringAsync(connectionStringName);

            if (MultiTenancyConsts.IsEnabled)
            {
                var tenantId = GetTenantId();
                if (!string.IsNullOrEmpty(tenantId))
                {
                    var tenantConnectionString = (await tenantDomainService.GetTenantConfigurationsAsync()).FirstOrDefault(x => x.Id == Guid.Parse(tenantId))?.ConnectionStrings?["MySql"];
                    if (!string.IsNullOrEmpty(tenantConnectionString))
                    {
                        connectionString = tenantConnectionString;
                    }
                }
            }

            var dbContextKey = $"{targetDbContextType.FullName}_{connectionString}";

            var databaseApi = unitOfWork.FindDatabaseApi(dbContextKey);

            if (databaseApi == null)
            {
                databaseApi = new EfCoreDatabaseApi(
                    await CreateDbContextAsync(unitOfWork, connectionStringName, connectionString)
                );

                unitOfWork.AddDatabaseApi(dbContextKey, databaseApi);
            }

            return (TDbContext)((EfCoreDatabaseApi)databaseApi).DbContext;
        }

        private string? GetTenantId()
        {
            return (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Tenant", out var tenantId) ?? false) ? tenantId : default;
        }
    }
}