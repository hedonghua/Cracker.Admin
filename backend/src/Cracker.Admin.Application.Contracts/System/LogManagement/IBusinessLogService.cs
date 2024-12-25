using Cracker.Admin.System.LogManagement.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System.LogManagement
{
    public interface IBusinessLogService : IApplicationService
    {
        /// <summary>
        /// 业务日志分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<BusinessLogListDto>> GetBusinessLogListAsync(BusinessLogQueryDto dto);

        /// <summary>
        /// 删除业务日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteBusinessLogsAsync(int[] ids);

        /// <summary>
        /// 新增业务日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddBusinessLogAsync(BusinessLogDto dto);
    }
}