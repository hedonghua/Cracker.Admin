using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
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
        private readonly IMemoryCache memoryCache;

        public TenantDomainService(IConfiguration configuration, IDapperFactory dapperFactory, IMemoryCache memoryCache)
        {
            this.configuration = configuration;
            this.dapperFactory = dapperFactory;
            this.memoryCache = memoryCache;
        }

        public async Task<List<TenantConfiguration>> GetTenantConfigurationsAsync()
        {
            var result = await memoryCache.GetOrCreateAsync("Tenants", async entry =>
            {
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
                            ["MySql"] = RSADecrypt(item.ConnectionString),
                            ["Redis"] = RSADecrypt(item.RedisConnection)
                        },
                        IsActive = false
                    });
                }

                return tenantConfigurations;
            });

            return result!;
        }

        private string RSADecrypt(string str)
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RSAKeys");
            var privateKeysPath = Path.Combine(dir, "PrivateKeys.txt");
            return EncryptionHelper.RSADecrypt(str, File.ReadAllText(privateKeysPath));
        }
    }
}