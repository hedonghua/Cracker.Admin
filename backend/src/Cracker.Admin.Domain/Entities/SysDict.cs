using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 字典表
    /// </summary>
    [Table("sys_dict")]
    public class SysDict : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 字典键
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? Key { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(1024)]
        public string? Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [StringLength(128)]
        public string? Label { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        [StringLength(128)]
        public string? GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string? Remark { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}