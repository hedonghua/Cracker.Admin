using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        [HasPermission("admin_system_businesslog_delete")]
        public async Task<AppResponse> DeleteBusinessLogsAsync([FromBody] long[] ids)
        {
            await _businessLogService.DeleteBusinessLogsAsync(ids);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 业务日志分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_businesslog_list")]
        public async Task<AppResponse<PagedResultDto<BusinessLogListDto>>> GetBusinessLogListAsync([FromQuery] BusinessLogQueryDto dto)
        {
            var data = await _businessLogService.GetBusinessLogListAsync(dto);
            return AppResponse.Data(data);
        }
    }
}