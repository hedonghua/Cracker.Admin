using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("sys_role")]
    [Comment("角色表")]
    public class SysRole : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [NotNull]
        [StringLength(64)]
        [Comment("角色名")]
        public string? RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        [Comment("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<SysUserRole>? UserRoles { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<SysRoleMenu>? RoleMenus { get; set; }
    }
}