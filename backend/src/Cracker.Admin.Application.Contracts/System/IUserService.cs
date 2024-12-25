using Cracker.Admin.System.Dtos;

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System
{
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddUserAsync(UserDto dto);

        /// <summary>
        /// 用户分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserListDto>> GetUserListAsync(UserQueryDto dto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(Guid id);

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AssignRoleAsync(AssignRoleDto dto);

        /// <summary>
        /// 切换用户启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SwitchUserEnabledStatusAsync(Guid id);

        /// <summary>
        /// 获取指定用户角色
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Guid[]> GetUserRoleIdsAsync(Guid uid);
    }
}