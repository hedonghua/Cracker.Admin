using System;

using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableColumnSearchDto : PageSearch
    {
        /// <summary>
        /// 生成表ID
        /// </summary>
        public Guid GenTableId { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string? ColumnName { get; set; }
    }
}