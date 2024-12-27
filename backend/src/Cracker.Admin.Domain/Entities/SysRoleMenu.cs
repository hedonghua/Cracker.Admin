using Microsoft.EntityFrameworkCore;
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
        [Comment("菜单ID")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Comment("角色ID")]
        public Guid RoleId { get; set; }

        public override object?[] GetKeys()
        {
            return [MenuId, RoleId];
        }
    }
}