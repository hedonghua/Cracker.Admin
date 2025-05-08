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
        /// 数据权限类型
        /// </summary>
        [Comment("数据权限类型（0全部1指定部门2部门及以下3本部门4仅本人）")]
        public int PowerDataType { get; set; } = 0;

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

        /// <summary>
        /// 角色查看部门（数据权限类型=1时，指定部门时才存入）
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<SysRoleDept>? RoleDepts { get; set; }
    }
}