using System.ComponentModel;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 员工表
    /// </summary>
    public class OrgEmployee : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工号
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? Code { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DefaultValue(0)]
        public int Sex { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(16)]
        public string? Phone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        public string? IdNo { get; set; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        [StringLength(512)]
        public string? FrontIdNoUrl { get; set; }

        /// <summary>
        /// 身份证背面
        /// </summary>
        [StringLength(512)]
        public string? BackIdNoUrl { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 现住址
        /// </summary>
        [StringLength(512)]
        public string? Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(64)]
        public string? Email { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime InTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 是否离职
        /// </summary>
        public bool IsOut { get; set; }

        /// <summary>
        /// 关联用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DeptId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public Guid PositionId { get; set; }
    }
}