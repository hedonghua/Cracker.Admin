using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System
{
    [Authorize]
    [ApiController]
    [Route("api/menu")]
    public class MenuController : AbpControllerBase
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
        [HasPermission("Sys.Menu.Add")]
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
        [HasPermission("Sys.Menu.List")]
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
        [HasPermission("Sys.Menu.Update")]
        public async Task<AppResponse> UpdateMenuAsync([FromBody] MenuDto dto)
        {
            await _menuService.UpdateMenuAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [HasPermission("Sys.Menu.Delete")]
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
        public async Task<AppResponse<List<MenuOptionTreeDto>>> GetMenuOptionsAsync(int type = 0)
        {
            var data = await _menuService.GetMenuOptionsAsync(type);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 生成受权限控制的路由
        /// </summary>
        /// <returns></returns>
        [HttpGet("power-routes")]
        public async Task<AppResponse<string>> GenPowerRoutes()
        {
            var data = await _menuService.GenPowerRoutes();
            return AppResponse.Data(JsonConvert.SerializeObject(data));
        }
    }
}