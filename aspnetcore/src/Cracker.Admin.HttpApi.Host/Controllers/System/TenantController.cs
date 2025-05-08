using Cracker.Admin.Attributes;
using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System
{
    [Authorize]
    [ApiController]
    [MustMainPower]
    [Route("api/[controller]/[action]")]
    public class TenantController : AbpControllerBase
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IAppResponse> AddTenantAsync([FromBody] TenantDto dto)
        {
            await tenantService.AddTenantAsync(dto);
            return A.Ok();
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission("Sys.Tenant.List")]
        public async Task<AppResponse<PagedResultStruct<TenantResultDto>>> GetTenantListAsync([FromQuery] TenantSearchDto dto)
        {
            var data = await tenantService.GetTenantListAsync(dto);
            return A.Data(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [HasPermission("Sys.Tenant.Update")]
        public async Task<IAppResponse> UpdateTenantAsync([FromBody] TenantDto dto)
        {
            await tenantService.UpdateTenantAsync(dto);
            return A.Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [HasPermission("Sys.Tenant.Delete")]
        public async Task<IAppResponse> DeleteTenantAsync(Guid id)
        {
            await tenantService.DeleteTenantAsync(id);
            return A.Ok();
        }

        [HttpGet]
        public async Task<AppResponse<string>> GetDecryptInfoAsync(Guid tenantId, string type)
        {
            var data = await tenantService.GetDecryptInfoAsync(tenantId, type);
            return A.Data(data);
        }
    }
}