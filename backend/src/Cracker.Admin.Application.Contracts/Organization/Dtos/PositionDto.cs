using System;

namespace Cracker.Admin.Organization.Dtos
{
    public class PositionDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 状态：1正常2停用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位分组
        /// </summary>
        public Guid? GroupId { get; set; }
    }
}