using System.Collections.Concurrent;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Extensions
{
    public static class TenantExtension
    {
        public static readonly ConcurrentDictionary<Guid, TenantConfiguration> Tenants = new ConcurrentDictionary<Guid, TenantConfiguration>();

        public static string? GetConnectionString(Guid? id)
        {
            Tenants.TryGetValue(id.GetValueOrDefault(), out var configuration);
            return configuration?.ConnectionStrings?["MySql"];
        }

        public static string? GetRedisConnection(Guid? id)
        {
            Tenants.TryGetValue(id.GetValueOrDefault(), out var configuration);
            return configuration?.ConnectionStrings?["Redis"];
        }
    }
}