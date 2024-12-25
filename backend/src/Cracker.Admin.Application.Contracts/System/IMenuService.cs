using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;

using Volo.Abp.Application.Services;

namespace Cracker.Admin.System
{
    public interface IMenuService : IApplicationService
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddMenuAsync(MenuDto dto);

        /// <summary>
        /// 菜单树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<List<MenuListDto>> GetMenuListAsync(MenuQueryDto dto);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateMenuAsync(MenuDto dto);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteMenusAsync(Guid[] ids);

        /// <summary>
        /// 获取菜单组成的选项树
        /// </summary>
        /// <returns></returns>
        Task<List<AppOptionTree>> GetMenuOptionsAsync();
    }
}