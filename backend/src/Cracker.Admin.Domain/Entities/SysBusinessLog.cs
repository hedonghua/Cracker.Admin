namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 业务日志
    /// </summary>
    public class SysBusinessLog : CreationAuditedEntity<long>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        public string? UserName { get; set; }

        /// <summary>
        /// 操作方法，全名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(512)]
        public string? Action { get; set; }

        /// <summary>
        /// http请求方式
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(16)]
        public string? HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(512)]
        public string? Url { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        [StringLength(64)]
        public string? Os { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [StringLength(64)]
        public string? Browser { get; set; }

        /// <summary>
        /// 操作节点名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        public string? NodeName { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [StringLength(32)]
        public string? Ip { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        [StringLength(256)]
        public string? Address { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        [StringLength(128)]
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 耗时，单位毫秒
        /// </summary>
        public int MillSeconds { get; set; }
    }
}