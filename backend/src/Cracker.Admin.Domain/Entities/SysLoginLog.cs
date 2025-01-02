using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 登录日志
    /// </summary>
    [Table("sys_loginlog")]
    [Comment("登录日志")]
    public class SysLoginLog : CreationAuditedEntity<long>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        [Comment("账号")]
        public string? UserName { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [StringLength(32)]
        [Comment("IP")]
        public string? Ip { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        [StringLength(256)]
        [Comment("登录地址")]
        public string? Address { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        [StringLength(64)]
        [Comment("系统")]
        public string? Os { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [StringLength(64)]
        [Comment("浏览器")]
        public string? Browser { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        [StringLength(128)]
        [Comment("浏览器操作信息")]
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [Comment("是否成功")]
        public bool IsSuccess { get; set; }
    }
}