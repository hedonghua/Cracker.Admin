using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class AssignMenuDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public Guid[]? MenuIds { get; set; }
    }
}