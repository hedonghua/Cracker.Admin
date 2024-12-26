using System.Collections.Generic;

using Cracker.Admin.Core;

namespace Cracker.Admin.Models
{
    public class PagedResultStruct<T> : PageSearch
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

        public PagedResultStruct(IPage page)
        {
            this.Page = page.Page;
            this.Size = page.Size;
        }
    }
}