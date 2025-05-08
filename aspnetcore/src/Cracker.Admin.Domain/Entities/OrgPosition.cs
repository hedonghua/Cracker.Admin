using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 职位表
    /// </summary>
    [Table("org_position")]
    [Comment("职位表")]
    public class OrgPosition : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 职位编号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        [Comment("职位编号")]
        public string? Code { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        [Comment("职位名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [NotNull]
        [Required]
        [Range(1, int.MaxValue)]
        [Comment("职级")]
        public int Level { get; set; }

        /// <summary>
        /// 状态：1正常2停用
        /// </summary>
        [Comment("状态：1正常2停用")]
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(512)]
        [Comment("描述")]
        public string? Description { get; set; }

        /// <summary>
        /// 职位分组
        /// </summary>
        [Comment("职位分组")]
        public Guid? GroupId { get; set; }
    }
}