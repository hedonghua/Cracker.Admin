using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 业务日志
    /// </summary>
    [Table("sys_businesslog")]
    [Comment("业务日志")]
    public class SysBusinessLog : CreationAuditedEntity<long>
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
        /// 操作方法，全名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(512)]
        [Comment("操作方法，全名")]
        public string? Action { get; set; }

        /// <summary>
        /// http请求方式
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(16)]
        [Comment("http请求方式")]
        public string? HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(512)]
        [Comment("请求地址")]
        public string? Url { get; set; }

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
        /// 操作节点名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        [Comment("操作节点名")]
        public string? NodeName { get; set; }

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
        /// 是否成功
        /// </summary>
        [Comment("是否成功")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        [Comment("操作信息")]
        [StringLength(128)]
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 耗时，单位毫秒
        /// </summary>
        [Comment("耗时，单位毫秒")]
        public int MillSeconds { get; set; }

        /// <summary>
        /// 请求跟踪ID
        /// </summary>
        [Comment("请求跟踪ID")]
        public string? RequestId { get; set; }
    }
}