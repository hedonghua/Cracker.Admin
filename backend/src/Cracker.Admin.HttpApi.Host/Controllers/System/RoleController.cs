using Microsoft.AspNetCore.Mvc;
using Cracker.Admin.CustomAttrs;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Cracker.Admin.Controllers.System
{
    [Route("/adm/role")]
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
        [AppResultFilter]
        [Permission("admin_system_role_add")]
        public Task<bool> AddRoleAsync([FromBody] RoleDto dto) => _roleService.AddRoleAsync(dto);

        /// <summary>
        /// 角色分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [Permission("admin_system_role_list")]
        public Task<PagedResultDto<RoleListDto>> GetRoleListAsync([FromQuery] RoleQueryDto dto)
        {
            return _roleService.GetRoleListAsync(dto);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [Permission("admin_system_role_update")]
        public Task<bool> UpdateRoleAsync([FromBody] RoleDto dto) => _roleService.UpdateRoleAsync(dto);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:Guid}")]
        [AppResultFilter]
        [Permission("admin_system_role_delete")]
        public Task<bool> DeleteRoleAsync(Guid id) => _roleService.DeleteRoleAsync(id);

        /// <summary>
        /// 分配菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("assign-menu")]
        [AppResultFilter]
        [Permission("admin_system_role_assignmenu")]
        public Task<bool> AssignMenuAsync([FromBody] AssignMenuDto dto) => _roleService.AssignMenuAsync(dto);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        [AppResultFilter]
        public Task<List<AppOption>> GetRoleOptionsAsync() => _roleService.GetRoleOptionsAsync();

        /// <summary>
        /// 获取指定角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("menus/{id:Guid}")]
        [AppResultFilter]
        public Task<Guid[]> GetRoleMenuIdsAsync(Guid id) => _roleService.GetRoleMenuIdsAsync(id);
    }
}