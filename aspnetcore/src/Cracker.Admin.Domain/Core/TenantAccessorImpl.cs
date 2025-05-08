using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Core
{
    public class TenantAccessorImpl : ICurrentTenantAccessor
    {
        public BasicTenantInfo? Current { get; set; }
    }
}