namespace Cracker.Admin.Entity
{
    /// <summary>
    /// 员工关联部门
    /// </summary>
    public class OrgDeptEmployee : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DeptId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// 是否主职位
        /// </summary>
        public bool IsMain {  get; set; }
    }
}