using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System
{
    public interface IRoleService : IApplicationService
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddRoleAsync(RoleDto dto);

        /// <summary>
        /// 角色分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<RoleListDto>> GetRoleListAsync(RoleQueryDto dto);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateRoleAsync(RoleDto dto);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteRoleAsync(Guid id);

        /// <summary>
        /// 分配菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AssignMenuAsync(AssignMenuDto dto);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        Task<List<AppOption>> GetRoleOptionsAsync();

        /// <summary>
        /// 获取指定角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid[]> GetRoleMenuIdsAsync(Guid id);
    }
}