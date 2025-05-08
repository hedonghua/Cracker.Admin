using Cracker.Admin.Models;

namespace Cracker.Admin.System.Dtos
{
    public class NotificationSearchDto : PageSearch
    {
        public string? Title { get; set; }

        public bool? IsReaded { get; set; }
    }
}