using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 字典数据表
    /// </summary>
    [Table("sys_dict_data")]
    public class SysDictData : FullAuditedEntity<Guid>
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
        [StringLength(256)]
        public string? Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [StringLength(128)]
        public string? Label { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [StringLength(128)]
        public string? DictType { get; set; }

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