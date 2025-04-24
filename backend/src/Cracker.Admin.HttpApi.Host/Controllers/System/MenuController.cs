using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.System
{
    [Route("api/menu")]
    public class MenuController : AdminController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("admin_system_menu_add")]
        public async Task<AppResponse> AddMenuAsync([FromBody] MenuDto dto)
        {
            await _menuService.AddMenuAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 菜单树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_menu_delete")]
        public async Task<AppResponse<List<MenuListDto>>> GetMenuListAsync([FromQuery] MenuQueryDto dto)
        {
            var data = await _menuService.GetMenuListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("admin_system_menu_update")]
        public async Task<AppResponse> UpdateMenuAsync([FromBody] MenuDto dto)
        {
            await _menuService.UpdateMenuAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [HasPermission("admin_system_menu_delete")]
        public async Task<AppResponse> DeleteMenusAsync([FromBody] Guid[] ids)
        {
            await _menuService.DeleteMenusAsync(ids);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 获取菜单组成的选项树
        /// </summary>
        /// <returns></returns>
        [HttpGet("menu-options")]
        public async Task<AppResponse<List<AppOptionTree>>> GetMenuOptionsAsync()
        {
            var data = await _menuService.GetMenuOptionsAsync();
            return AppResponse.Data(data);
        }
    }
}