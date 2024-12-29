using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 部门表
    /// </summary>
    [Comment("部门表")]
    public class OrgDept : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        [Comment("部门编号")]
        public string? Code { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        [Comment("部门名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(512)]
        [Comment("描述")]
        public string? Description { get; set; }

        /// <summary>
        /// 状态：1正常2停用
        /// </summary>
        [Comment("状态：1正常2停用")]
        public int Status { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Comment("负责人")]
        public Guid? CuratorId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(64)]
        [Comment("邮箱")]
        public string? Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(64)]
        [Comment("电话")]
        public string? Phone { get; set; }

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

        /// <summary>
        /// 层级
        /// </summary>
        [DefaultValue(0)]
        [Comment("层级")]
        public int Layer { get; set; }
    }
}