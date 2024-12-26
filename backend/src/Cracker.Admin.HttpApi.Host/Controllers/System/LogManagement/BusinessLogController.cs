using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.CustomAttrs;
using Cracker.Admin.Filters;
using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;

using Volo.Abp.Application.Dtos;

namespace Cracker.Admin.Controllers.System.LogManagement
{
    [Route("api/business-log")]
    public class BusinessLogController : AdminController
    {
        private readonly IBusinessLogService _businessLogService;

        public BusinessLogController(IBusinessLogService businessLogService)
        {
            _businessLogService = businessLogService;
        }

        /// <summary>
        /// 删除业务日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AppResultFilter]
        [Permission("admin_system_businesslog_delete")]
        public Task<bool> DeleteBusinessLogsAsync([FromBody] int[] ids) => _businessLogService.DeleteBusinessLogsAsync(ids);

        /// <summary>
        /// 业务日志分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [Permission("admin_system_businesslog_list")]
        public Task<PagedResultDto<BusinessLogListDto>> GetBusinessLogListAsync([FromQuery] BusinessLogQueryDto dto) => _businessLogService.GetBusinessLogListAsync(dto);
    }
}