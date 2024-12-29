using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableSearchDto : PageSearch
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }
    }
}