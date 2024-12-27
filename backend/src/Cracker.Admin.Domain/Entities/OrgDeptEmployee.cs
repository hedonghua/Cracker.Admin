using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 员工关联部门
    /// </summary>
    [Comment("员工关联部门")]
    public class OrgDeptEmployee : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Comment("员工ID")]
        public Guid EmployeeId { get; set; }

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

        /// <summary>
        /// 是否主职位
        /// </summary>
        [Comment("是否主职位")]
        public bool IsMain { get; set; }
    }
}