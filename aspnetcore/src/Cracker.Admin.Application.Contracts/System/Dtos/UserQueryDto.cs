using Cracker.Admin.Models;

namespace Cracker.Admin.System.Dtos
{
    public class UserQueryDto : PageSearch
    {
        public string? UserName { get; set; }
    }
}