using System.Collections.Concurrent;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Extensions
{
    public static class TenantExtension
    {
        public static readonly ConcurrentDictionary<string, TenantConfiguration> Tenants = new ConcurrentDictionary<string, TenantConfiguration>();

        public static string? GetConnectionString(string? name)
        {
            Tenants.TryGetValue(name ?? "Default", out var configuration);
            return configuration?.ConnectionStrings?["MySql"];
        }

        public static string? GetRedisConnection(string? name)
        {
            Tenants.TryGetValue(name ?? "Default", out var configuration);
            return configuration?.ConnectionStrings?["Redis"];
        }
    }
}