using System.Threading.Tasks;

using Cracker.Admin.Models;
using Cracker.Admin.Monitor.Dtos;

namespace Cracker.Admin.Monitor
{
    public interface IOnlineUserService
    {
        Task<PagedResultStruct<OnlineUserResultDto>> GetOnlineUserListAsync(OnlineUserSearchDto dto);

        Task LogoutAsync(string userId);
    }
}