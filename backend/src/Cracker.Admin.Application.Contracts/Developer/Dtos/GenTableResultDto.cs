using System;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableResultDto
    {
        public Guid GenTableId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 业务名
        /// </summary>
        public string? BusinessName { get; set; }

        /// <summary>
        /// 实体名
        /// </summary>
        public string? EntityName { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        public string? ModuleName { get; set; }
    }
}