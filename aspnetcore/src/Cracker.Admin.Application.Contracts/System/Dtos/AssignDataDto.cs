using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class AssignDataDto
    {
        [Required]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 数据权限类型（0全部1指定部门2部门及以下3本部门4仅本人）
        /// </summary>
        [Required]
        public int PowerDataType { get; set; } = 0;

        public Guid[]? DeptIds { get; set; }
    }
}