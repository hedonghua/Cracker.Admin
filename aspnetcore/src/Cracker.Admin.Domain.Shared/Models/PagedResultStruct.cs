using Cracker.Admin.Core;
using System.Collections.Generic;

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
            this.Current = page.Current;
            this.PageSize = page.PageSize;
        }

        public PagedResultStruct(IPage page, int totalCount, List<T> items)
        {
            this.Current = page.Current;
            this.PageSize = page.PageSize;
            Items = items;
            TotalCount = totalCount;
        }
    }
}