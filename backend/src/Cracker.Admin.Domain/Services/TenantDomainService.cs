using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Dapper;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Services
{
    public class TenantDomainService : DomainService
    {
        private readonly IConfiguration configuration;
        private readonly IDapperFactory dapperFactory;
        private readonly IDatabase database;

        public TenantDomainService(IConfiguration configuration, IDapperFactory dapperFactory, IDatabase database)
        {
            this.configuration = configuration;
            this.dapperFactory = dapperFactory;
            this.database = database;
        }

        public async Task<List<TenantConfiguration>> GetTenantConfigurationsAsync()
        {
            var cache = await database.GetObjectAsync<List<TenantConfiguration>>("Tenants");
            if (cache != null) return cache;

            var defaultConnectionString = configuration.GetConnectionString("Default");
            var connection = dapperFactory.CreateInstance(defaultConnectionString!);

            var list = (await connection.QueryAsync<SysTenant>("select * from sys_tenant")).ToList();
            var tenantConfigurations = new List<TenantConfiguration>();
            foreach (var item in list)
            {
                tenantConfigurations.Add(new TenantConfiguration
                {
                    Id = item.Id,
                    Name = item.Name,
                    ConnectionStrings = new Volo.Abp.Data.ConnectionStrings
                    {
                        ["MySql"] = item.ConnectionString,
                        ["Redis"] = item.RedisConnection
                    },
                    IsActive = false
                });
            }

            await database.SetObjectAsync("Tenants", tenantConfigurations);

            return tenantConfigurations;
        }
    }
}