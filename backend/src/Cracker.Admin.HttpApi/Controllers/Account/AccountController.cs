using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.Account;
using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;

namespace Cracker.Admin.Controllers.Account
{
    [Route("/adm/account")]
    public class AccountController : AdminController
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
        [AppResultFilter]
        [HttpPost("login")]
        public Task<LoginResultDto> LoginAsync([FromBody] LoginDto dto) => _accountService.LoginAsync(dto);

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [AppResultFilter]
        [HttpPost("refresh-token")]
        public Task<TokenResultDto> GetAccessTokenAsync(string refreshToken) => _accountService.GetAccessTokenAsync(refreshToken);

        /// <summary>
        /// 当前用户信息、角色、权限
        /// </summary>
        /// <returns></returns>
        [AppResultFilter]
        [HttpGet("userinfo")]
        public Task<UserInfoDto> GetUserInfoAsync() => _accountService.GetUserInfoAsync();

        /// <summary>
        /// 获取当前用户可访问路由
        /// </summary>
        /// <returns></returns>
        [AppResultFilter]
        [HttpGet("menus")]
        public Task<List<FrontRoute>> GetFrontRoutes(int listStruct) => _accountService.GetFrontRoutes(listStruct);

        /// <summary>
        /// 修改个人基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AppResultFilter]
        [HttpPut("update-info")]
        public Task<bool> UpdateUserInfoAsync([FromBody]PersonalInfoDto dto) => _accountService.UpdateUserInfoAsync(dto);

        /// <summary>
        /// 修改个人密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AppResultFilter]
        [HttpPut("update-pwd")]
        public Task<bool> UpdateUserPwdAsync([FromBody] UserPwdDto dto) => _accountService.UpdateUserPwdAsync(dto);

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [AppResultFilter]
        [HttpPost("signout")]
        public Task<bool> SignOutAsync() => _accountService.SignOutAsync();
    }
}