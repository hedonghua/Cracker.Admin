using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.CustomAttrs;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;

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
        [AppResultFilter]
        [Permission("admin_system_menu_add")]
        public Task<bool> AddMenuAsync([FromBody] MenuDto dto) => _menuService.AddMenuAsync(dto);

        /// <summary>
        /// 菜单树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [Permission("admin_system_menu_delete")]
        public Task<List<MenuListDto>> GetMenuListAsync([FromQuery] MenuQueryDto dto) => _menuService.GetMenuListAsync(dto);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [Permission("admin_system_menu_update")]
        public Task<bool> UpdateMenuAsync([FromBody] MenuDto dto) => _menuService.UpdateMenuAsync(dto);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AppResultFilter]
        [Permission("admin_system_menu_delete")]
        public Task<bool> DeleteMenusAsync([FromBody] Guid[] ids) => _menuService.DeleteMenusAsync(ids);

        /// <summary>
        /// 获取菜单组成的选项树
        /// </summary>
        /// <returns></returns>
        [HttpGet("menu-options")]
        [AppResultFilter]
        public Task<List<AppOptionTree>> GetMenuOptionsAsync() => _menuService.GetMenuOptionsAsync();
    }
}