using Volo.Abp.Domain.Entities;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class SysUserRole : Entity<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }
    }
}