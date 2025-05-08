using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System
{
    public interface IDictDataService : IApplicationService
    {
        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddDictDataAsync(DictDataDto dto);

        /// <summary>
        /// 字典分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<DictDataListDto>> GetDictDataListAsync(DictDataQueryDto dto);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateDictDataAsync(DictDataDto dto);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteDictDataAsync(Guid[] ids);
    }
}