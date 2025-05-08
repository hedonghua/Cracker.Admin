using Cracker.Admin.Attributes;
using Cracker.Admin.Core;
using Cracker.Admin.Models;
using Cracker.Admin.Monitor;
using Cracker.Admin.Monitor.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Monitor
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OnlineUserController : AbpControllerBase
    {
        private readonly IOnlineUserService onlineUserService;

        public OnlineUserController(IOnlineUserService onlineUserService)
        {
            this.onlineUserService = onlineUserService;
        }

        [HttpGet]
        [HasPermission("Monitor.OnlineUser")]
        public async Task<AppResponse<PagedResultStruct<OnlineUserResultDto>>> GetOnlineUserListAsync([FromQuery] OnlineUserSearchDto dto)
        {
            var data = await onlineUserService.GetOnlineUserListAsync(dto);
            return A.Data(data);
        }

        [HttpPost]
        [HasPermission("Monitor.Logout")]
        public async Task<IAppResponse> LogoutAsync(string key)
        {
            await onlineUserService.LogoutAsync(key);
            return A.Ok();
        }
    }
}