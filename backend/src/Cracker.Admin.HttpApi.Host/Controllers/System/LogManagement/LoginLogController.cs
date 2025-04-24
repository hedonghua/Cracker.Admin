using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Cracker.Admin.Controllers.System.LogManagement
{
    [Route("api/login-log")]
    public class LoginLogController : AdminController
    {
        private readonly ILoginLogService _loginLogService;

        public LoginLogController(ILoginLogService loginLogService)
        {
            _loginLogService = loginLogService;
        }

        /// <summary>
        /// 删除登录日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [HasPermission("admin_system_loginlog_delete")]
        public async Task<AppResponse> DeleteLoginLogsAsync([FromBody] long[] ids)
        {
            await _loginLogService.DeleteLoginLogsAsync(ids);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 登录日志分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_loginlog_list")]
        public async Task<AppResponse<PagedResultDto<LoginLogListDto>>> GetLoginLogListAsync([FromQuery] LoginLogQueryDto dto)
        {
            var data = await _loginLogService.GetLoginLogListAsync(dto);
            return AppResponse.Data(data);
        }
    }
}