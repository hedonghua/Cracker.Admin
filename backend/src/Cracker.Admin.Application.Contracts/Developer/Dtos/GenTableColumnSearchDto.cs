using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableColumnSearchDto : PageSearch
    {
        public string? ColumnName { get; set; }
    }
}