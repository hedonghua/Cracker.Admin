using System.ComponentModel;

namespace Cracker.Admin.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class SysUser : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [NotNull]
        [StringLength(32)]
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [NotNull]
        [StringLength(512)]
        public string? Password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [NotNull]
        [StringLength(256)]
        public string? PasswordSalt { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        public string? Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [NotNull]
        [Required]
        [DefaultValue(0)]
        public int Sex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }
    }
}