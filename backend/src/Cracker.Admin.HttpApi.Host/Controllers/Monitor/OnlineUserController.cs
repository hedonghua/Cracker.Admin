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
        public async Task<IAppResponse> GetOnlineUserListAsync([FromQuery] OnlineUserSearchDto dto)
        {
            var data = await onlineUserService.GetOnlineUserListAsync(dto);
            return ResultHelper.Ok(data);
        }

        [HttpPost]
        public async Task<IAppResponse> LogoutAsync(string key)
        {
            await onlineUserService.LogoutAsync(key);
            return ResultHelper.Ok();
        }
    }
}