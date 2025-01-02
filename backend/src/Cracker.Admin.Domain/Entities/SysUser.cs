using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("sys_user")]
    public class SysUser : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [NotNull]
        [StringLength(32)]
        [Comment("用户名")]
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [NotNull]
        [StringLength(512)]
        [Comment("密码")]
        public string? Password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [NotNull]
        [StringLength(256)]
        [Comment("密码盐")]
        public string? PasswordSalt { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        [Comment("头像")]
        public string? Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [NotNull]
        [Required]
        [Comment("昵称")]
        [StringLength(64)]
        public string? NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [NotNull]
        [Required]
        [Comment("性别")]
        [DefaultValue(0)]
        public int Sex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [Comment("是否启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<SysUserRole>? UserRoles { get; set; }
    }
}