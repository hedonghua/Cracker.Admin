using Cracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Cracker.Admin.Organization.Dtos;

namespace Cracker.Admin.Organization
{
    public interface IPositionGroupService : IApplicationService
    {
        /// <summary>
        /// 新增职位分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddPositionGroupAsync(PositionGroupDto dto);

        /// <summary>
        /// 职位分组分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<List<PositionGroupListDto>> GetPositionGroupListAsync(PositionGroupQueryDto dto);

        /// <summary>
        /// 修改职位分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdatePositionGroupAsync(PositionGroupDto dto);

        /// <summary>
        /// 删除职位分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePositionGroupAsync(Guid id);

        /// <summary>
        /// 获取职位分组
        /// </summary>
        /// <returns></returns>
        Task<List<AppOption>> GetPositionGroupOptionsAsync();
    }
}