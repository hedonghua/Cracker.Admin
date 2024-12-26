using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class DatabaseableSearchDto : PageSearch
    {
        public string? TableName { get; set; }
    }
}