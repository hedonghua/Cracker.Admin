using System;

namespace Cracker.Admin.System.Dtos
{
    public class AssignMenuDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid[]? MenuIds { get; set; }
    }
}