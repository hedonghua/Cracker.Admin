using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System
{
    [Authorize]
    [ApiController]
    [Route("/api/user")]
    public class UserController : AbpControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AppBusinessLogFilter("新增用户")]
        [HasPermission("Sys.User.Add")]
        public async Task<AppResponse> AddUserAsync([FromBody] UserDto dto)
        {
            await _userService.AddUserAsync(dto);
            return A.Ok();
        }

        /// <summary>
        /// 用户分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("Sys.User.List")]
        public async Task<AppResponse<PagedResultDto<UserListDto>>> GetUserListAsync([FromQuery] UserQueryDto dto)
        {
            var data = await _userService.GetUserListAsync(dto);
            return A.Data(data);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:Guid}")]
        [AppBusinessLogFilter("删除用户")]
        [HasPermission("Sys.User.Delete")]
        public async Task<AppResponse> DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("assign-role")]
        [AppBusinessLogFilter("分配角色")]
        [HasPermission("Sys.User.AssignRole")]
        public async Task<AppResponse> AssignRoleAsync([FromBody] AssignRoleDto dto)
        {
            await _userService.AssignRoleAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 切换用户启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("change-enabled/{id:Guid}")]
        [AppBusinessLogFilter("切换用户状态")]
        [HasPermission("Sys.User.SwitchEnabledStatus")]
        public async Task<AppResponse> SwitchUserEnabledStatusAsync(Guid id)
        {
            await _userService.SwitchUserEnabledStatusAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 获取指定用户角色
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet("roles/{uid:Guid}")]
        public async Task<AppResponse<Guid[]>> GetUserRoleIdsAsync(Guid uid)
        {
            var data = await _userService.GetUserRoleIdsAsync(uid);
            return A.Data(data);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("reset-pwd")]
        [HasPermission("Sys.User.ResetPwd")]
        public async Task<AppResponse> ResetUserPasswordAsync([FromBody] ResetUserPwdDto dto)
        {
            await _userService.ResetUserPasswordAsync(dto);
            return A.Ok();
        }
    }
}