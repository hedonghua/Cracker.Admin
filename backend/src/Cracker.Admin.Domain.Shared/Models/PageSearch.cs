using Cracker.Admin.Core;

namespace Cracker.Admin.Models
{
    public class PageSearch : IPage
    {
        public int Size { get; set; }
        public int Page { get; set; }
    }
}