using Cracker.Admin.Models;

namespace Cracker.Admin.Monitor.Dtos
{
    public class OnlineUserSearchDto : PageSearch
    {
        public string? UserName { get; set; }
    }
}