using Volo.Abp.Domain.Entities;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    public class SysRoleMenu : Entity
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        public override object?[] GetKeys()
        {
            return [MenuId, RoleId];
        }
    }
}