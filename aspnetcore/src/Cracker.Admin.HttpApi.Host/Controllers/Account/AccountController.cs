using Cracker.Admin.Account;
using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Account
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : AbpControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AppResponse<LoginResultDto>> LoginAsync([FromBody] LoginDto dto)
        {
            var data = await _accountService.LoginAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<AppResponse<TokenResultDto>> GetAccessTokenAsync(string refreshToken)
        {
            var data = await _accountService.GetAccessTokenAsync(refreshToken);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 当前用户信息、角色、权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("userinfo")]
        public async Task<AppResponse<UserInfoDto>> GetUserInfoAsync()
        {
            var data = await _accountService.GetUserInfoAsync();
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 获取侧边栏菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("menus")]
        public async Task<AppResponse<List<FrontendMenu>>> GetFrontRoutes(int listStruct)
        {
            var data = await _accountService.GetFrontMenus(listStruct);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改个人基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-info")]
        public async Task<AppResponse> UpdateUserInfoAsync([FromBody] PersonalInfoDto dto)
        {
            await _accountService.UpdateUserInfoAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 修改个人密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update-pwd")]
        public async Task<AppResponse> UpdateUserPwdAsync([FromBody] UserPwdDto dto)
        {
            await _accountService.UpdateUserPwdAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost("signout")]
        public async Task<AppResponse> SignOutAsync()
        {
            await _accountService.SignOutAsync();
            return AppResponse.Ok();
        }
    }
}