using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 租户表
    /// </summary>
    [Comment("租户表")]
    public class SysTenant : AuditedEntity<Guid>
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [StringLength(128)]
        [Comment("租户名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Required]
        [StringLength(512)]
        [Comment("连接字符串")]
        public string? ConnectionString { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        [Comment("备注")]
        public string? Remark { get; set; }
    }
}