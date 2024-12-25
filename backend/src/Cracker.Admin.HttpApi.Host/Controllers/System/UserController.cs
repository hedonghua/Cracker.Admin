using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.CustomAttrs;
using Cracker.Admin.Filters;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;

using Volo.Abp.Application.Dtos;

namespace Cracker.Admin.Controllers.System
{
    [Route("/adm/user")]
    public class UserController : AdminController
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
        [AppResultFilter]
        [AppBusinessLogFilter("新增用户")]
        [Permission("admin_system_user_add")]
        public Task<bool> AddUserAsync([FromBody] UserDto dto) => _userService.AddUserAsync(dto);

        /// <summary>
        /// 用户分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [Permission("admin_system_user_list")]
        public Task<PagedResultDto<UserListDto>> GetUserListAsync([FromQuery] UserQueryDto dto)
            => _userService.GetUserListAsync(dto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:Guid}")]
        [AppResultFilter]
        [AppBusinessLogFilter("删除用户")]
        [Permission("admin_system_user_delete")]
        public Task<bool> DeleteUserAsync(Guid id) => _userService.DeleteUserAsync(id);

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("assign-role")]
        [AppResultFilter]
        [AppBusinessLogFilter("分配角色")]
        [Permission("admin_system_user_assignrole")]
        public Task<bool> AssignRoleAsync([FromBody] AssignRoleDto dto) => _userService.AssignRoleAsync(dto);

        /// <summary>
        /// 切换用户启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("change-enabled/{id:Guid}")]
        [AppResultFilter]
        [AppBusinessLogFilter("切换用户状态")]
        [Permission("admin_system_user_changeenabled")]
        public Task<bool> SwitchUserEnabledStatusAsync(Guid id) => _userService.SwitchUserEnabledStatusAsync(id);

        /// <summary>
        /// 获取指定用户角色
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet("roles/{uid:Guid}")]
        [AppResultFilter]
        public Task<Guid[]> GetUserRoleIdsAsync(Guid uid) => _userService.GetUserRoleIdsAsync(uid);
    }
}