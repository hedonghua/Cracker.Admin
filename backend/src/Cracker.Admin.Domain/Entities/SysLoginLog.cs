namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class SysLoginLog : CreationAuditedEntity<long>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        public string? UserName { get; set; }

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
        /// 操作信息
        /// </summary>
        [StringLength(128)]
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        public void SetCreationTime()
        {
            CreationTime = DateTime.Now;
        }
    }
}