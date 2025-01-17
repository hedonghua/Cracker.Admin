using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 字典类型表
    /// </summary>
    [Table("sys_dict_type")]
    public class SysDictType : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [StringLength(128)]
        public string? Name { get; set; }

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
        /// 是否开启
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}