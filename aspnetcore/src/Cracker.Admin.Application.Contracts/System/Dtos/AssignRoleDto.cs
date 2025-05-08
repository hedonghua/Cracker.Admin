using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class AssignRoleDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public Guid[]? RoleIds { get; set; }
    }
}