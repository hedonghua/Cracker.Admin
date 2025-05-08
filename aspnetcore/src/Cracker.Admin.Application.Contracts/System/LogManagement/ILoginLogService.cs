using System;
using System.Threading.Tasks;

using Cracker.Admin.System.LogManagement.Dtos;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.System.LogManagement
{
    public interface ILoginLogService : IApplicationService
    {
        /// <summary>
        /// 登录日志分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultDto<LoginLogListDto>> GetLoginLogListAsync(LoginLogQueryDto dto);

        /// <summary>
        /// 删除登录日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteLoginLogsAsync(long[] ids);
    }
}