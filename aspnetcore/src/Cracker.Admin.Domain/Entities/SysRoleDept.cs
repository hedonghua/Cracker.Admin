using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Cracker.Admin.Entities
{
    [Table("sys_role_dept")]
    public class SysRoleDept : Entity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Comment("角色ID")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Comment("部门ID")]
        public Guid DeptId { get; set; }

        public override object?[] GetKeys()
        {
            return [RoleId, DeptId];
        }
    }
}