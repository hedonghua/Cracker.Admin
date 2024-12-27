using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Comment("系统配置")]
    [Index(nameof(Key), IsUnique = true)]
    public class SysConfig : AuditedEntity<Guid>
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [NotNull]
        [Required]
        [Comment("配置名称")]
        [StringLength(256)]
        public string? Name { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [NotNull]
        [Required]
        [Comment("配置键名")]
        [StringLength(128)]
        public string? Key { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        [NotNull]
        [Required]
        [Comment("配置键值")]
        public string? Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [StringLength(512)]
        public string? Remark { get; set; }
    }
}