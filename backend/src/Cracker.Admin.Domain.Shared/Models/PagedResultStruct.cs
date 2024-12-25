using System.Collections.Generic;

namespace Cracker.Admin.Models
{
    public class PagedResultStruct<T>
    {
        /// <summary>
        /// 分页后数据
        /// </summary>
        public List<T>? Items { get; set; }

        /// <summary>
        /// 总条目
        /// </summary>
        public int TotalCount { get; set; }

        public PagedResultStruct()
        { }

        public PagedResultStruct(int totalCount, List<T> items)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}