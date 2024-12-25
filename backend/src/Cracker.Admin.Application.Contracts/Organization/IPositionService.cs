using Cracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Cracker.Admin.Organization.Dtos;

namespace Cracker.Admin.Organization
{
    public interface IPositionService : IApplicationService
    {
        /// <summary>
        /// 新增职位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddPositionAsync(PositionDto dto);

        /// <summary>
        /// 职位分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<PositionListDto>> GetPositionListAsync(PositionQueryDto dto);

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdatePositionAsync(PositionDto dto);

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletePositionAsync(Guid id);

        /// <summary>
        /// 职位分组+职位树
        /// </summary>
        /// <returns></returns>
        Task<List<AppOptionTree>> GetPositionTreeOptionAsync();
    }
}