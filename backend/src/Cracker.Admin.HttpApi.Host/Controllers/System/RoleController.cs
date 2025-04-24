using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Cracker.Admin.Controllers.System
{
    [Route("api/role")]
    public class RoleController : AdminController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("admin_system_role_add")]
        public async Task<AppResponse> AddRoleAsync([FromBody] RoleDto dto)
        {
            await _roleService.AddRoleAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 角色分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_role_list")]
        public async Task<AppResponse<PagedResultDto<RoleListDto>>> GetRoleListAsync([FromQuery] RoleQueryDto dto)
        {
            var data = await _roleService.GetRoleListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("admin_system_role_update")]
        public async Task<AppResponse> UpdateRoleAsync([FromBody] RoleDto dto)
        {
            await _roleService.UpdateRoleAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:Guid}")]
        [HasPermission("admin_system_role_delete")]
        public async Task<AppResponse> DeleteRoleAsync(Guid id)
        {
            await _roleService.DeleteRoleAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 分配菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("assign-menu")]
        [HasPermission("admin_system_role_assignmenu")]
        public async Task<AppResponse> AssignMenuAsync([FromBody] AssignMenuDto dto)
        {
            await _roleService.AssignMenuAsync(dto);
            return A.Ok();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        public async Task<AppResponse<List<AppOption>>> GetRoleOptionsAsync()
        {
            var data = await _roleService.GetRoleOptionsAsync();
            return A.Data(data);
        }

        /// <summary>
        /// 获取指定角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("menus/{id:Guid}")]
        public async Task<AppResponse<Guid[]>> GetRoleMenuIdsAsync(Guid id)
        {
            var data = await _roleService.GetRoleMenuIdsAsync(id);
            return A.Data(data);
        }
    }
}