using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.Organization.Dtos
{
    public class PositionGroupDto
    {
        public Guid? Id { get; set; }
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required]
        public string? GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
    }
}