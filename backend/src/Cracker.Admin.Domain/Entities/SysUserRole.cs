using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    [Table("sys_userrole")]
    [Comment("用户角色关联表")]
    public class SysUserRole : Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Comment("用户ID")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Comment("角色ID")]
        public Guid RoleId { get; set; }

        public override object?[] GetKeys()
        {
            return [UserId, RoleId];
        }
    }
}