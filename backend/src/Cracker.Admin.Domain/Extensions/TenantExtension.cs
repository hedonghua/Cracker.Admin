using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Extensions
{
    public static class TenantExtension
    {
        public static string GetConnectionString(this ICurrentTenant tenant)
        {
            return "";
        }

        public static string GetRedisConnection(this ICurrentTenant tenant)
        {
            return "";
        }
    }
}