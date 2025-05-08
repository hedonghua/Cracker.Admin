using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 字典类型表
    /// </summary>
    [Table("sys_dict_type")]
    [Index(nameof(DictType), IsUnique = true)]
    public class SysDictType : AuditedEntity<Guid>
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [NotNull]
        [Required]
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

        [ForeignKey("DictType")]
        public virtual ICollection<SysDictData>? DictDatas { get; set; }
    }
}