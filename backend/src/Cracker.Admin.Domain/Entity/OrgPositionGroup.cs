namespace Cracker.Admin.Entity
{
    /// <summary>
    /// 职位分组
    /// </summary>
    public class OrgPositionGroup : AuditedEntity<Guid>
    {
        /// <summary>
        /// 分组名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string? Remark { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 层级父ID
        /// </summary>
        [StringLength(1024)]
        public string? ParentIds { get; set; }
    }
}