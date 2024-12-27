using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 员工表
    /// </summary>
    [Comment("员工表")]
    public class OrgEmployee : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        [Comment("工号")]
        public string? Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        [Comment("姓名")]
        public string? Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DefaultValue(0)]
        [Comment("性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(16)]
        [Comment("手机号码")]
        public string? Phone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        [Comment("身份证")]
        public string? IdNo { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        [StringLength(512)]
        [Comment("身份证正面")]
        public string? FrontIdNoUrl { get; set; }

        /// <summary>
        /// 身份证背面
        /// </summary>
        [StringLength(512)]
        [Comment("身份证背面")]
        public string? BackIdNoUrl { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        [Comment("生日")]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 现住址
        /// </summary>
        [StringLength(512)]
        [Comment("现住址")]
        public string? Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(64)]
        [Comment("邮箱")]
        public string? Email { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        [Comment("入职时间")]
        public DateTime InTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        [Comment("离职时间")]
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 是否离职
        /// </summary>
        [Comment("是否离职")]
        public bool IsOut { get; set; }

        /// <summary>
        /// 关联用户ID
        /// </summary>
        [Comment("关联用户ID")]
        public Guid? UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Comment("部门ID")]
        public Guid DeptId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        [Comment("职位ID")]
        public Guid PositionId { get; set; }
    }
}