namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 职位表
    /// </summary>
    public class OrgPosition : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 职位编号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        public string? Code { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [NotNull]
        [Required]
        [Range(1, int.MaxValue)]
        public int Level { get; set; }

        /// <summary>
        /// 状态：1正常2停用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(512)]
        public string? Description { get; set; }

        /// <summary>
        /// 职位分组
        /// </summary>
        public Guid? GroupId { get; set; }
    }
}