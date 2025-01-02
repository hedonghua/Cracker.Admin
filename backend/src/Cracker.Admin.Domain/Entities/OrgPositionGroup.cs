using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 职位分组
    /// </summary>
    [Table("org_positiongroup")]
    [Comment("职位分组")]
    public class OrgPositionGroup : AuditedEntity<Guid>
    {
        /// <summary>
        /// 分组名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        [Comment("分组名")]
        public string? GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        [Comment("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Comment("父ID")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 层级父ID
        /// </summary>
        [StringLength(1024)]
        [Comment("层级父ID")]
        public string? ParentIds { get; set; }
    }
}