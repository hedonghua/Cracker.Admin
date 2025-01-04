using System.Threading.Tasks;

using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.Monitor;
using Cracker.Admin.Monitor.Dtos;

using Microsoft.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Monitor
{
    [Route("api/[controller]/[action]")]
    public class OnlineUserController : AdminController
    {
        private readonly IOnlineUserService onlineUserService;

        public OnlineUserController(IOnlineUserService onlineUserService)
        {
            this.onlineUserService = onlineUserService;
        }

        [HttpGet]
        public async Task<IAppResult> GetOnlineUserListAsync([FromQuery] OnlineUserSearchDto dto)
        {
            var data = await onlineUserService.GetOnlineUserListAsync(dto);
            return ResultHelper.Ok(data);
        }

        [HttpPost]
        public async Task<IAppResult> LogoutAsync(string userId)
        {
            await onlineUserService.LogoutAsync(userId);
            return ResultHelper.Ok();
        }
    }
}