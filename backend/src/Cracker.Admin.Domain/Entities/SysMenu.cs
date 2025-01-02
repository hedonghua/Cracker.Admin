using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Cracker.Admin.Enums;
using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("sys_menu")]
    [Comment("菜单表")]
    public class SysMenu : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 显示标题/名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        [Comment("显示标题/名称")]
        public string? Title { get; set; }

        /// <summary>
        /// 组件名
        /// </summary>
        [StringLength(32)]
        [Comment("组件名")]
        public string? Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(64)]
        [Comment("图标")]
        public string? Icon { get; set; }

        /// <summary>
        /// 路由/地址
        /// </summary>
        [StringLength(256)]
        [Comment("路由/地址")]
        public string? Path { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        [Comment("功能类型")]
        public FunctionType FunctionType { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        [StringLength(128)]
        [Comment("授权码")]
        public string? Permission { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Comment("父级ID")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Comment("是否隐藏")]
        public bool Hidden { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        [ForeignKey("MenuId")]
        public virtual ICollection<SysRoleMenu>? RoleMenus { get; set; }
    }
}