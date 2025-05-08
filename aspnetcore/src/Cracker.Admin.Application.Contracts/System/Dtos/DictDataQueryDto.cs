using Cracker.Admin.Models;

namespace Cracker.Admin.System.Dtos
{
    public class DictDataQueryDto : PageSearch
    {
        /// <summary>
        /// 字典键
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string? DictType { get; set; }
    }
}