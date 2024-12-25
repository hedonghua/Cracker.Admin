using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cracker.Admin.System.Dtos
{
    public class AssignRoleDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid[]? RoleIds { get; set; }
    }
}