namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [NotNull]
        [StringLength(64)]
        public string? RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string? Remark { get; set; }
    }
}