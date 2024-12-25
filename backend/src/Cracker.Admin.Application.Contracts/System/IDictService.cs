using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System
{
    public interface IDictService : IApplicationService
    {
        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddDictAsync(DictDto dto);

        /// <summary>
        /// 字典分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<DictListDto>> GetDictListAsync(DictQueryDto dto);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateDictAsync(DictDto dto);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteDictAsync(Guid[] ids);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshCacheAsync();
    }
}