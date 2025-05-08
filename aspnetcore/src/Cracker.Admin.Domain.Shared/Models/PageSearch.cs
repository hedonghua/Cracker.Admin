using Cracker.Admin.Core;

namespace Cracker.Admin.Models
{
    public class PageSearch : IPage
    {
        public int PageSize { get; set; }
        public int Current { get; set; }
    }
}