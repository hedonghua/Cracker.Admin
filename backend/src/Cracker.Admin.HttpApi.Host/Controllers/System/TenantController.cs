using Cracker.Admin.Attributes;
using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.System
{
    [MustMainPower]
    [Route("api/[controller]/[action]")]
    public class TenantController : AdminController
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
            return ResultHelper.Ok();
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IAppResponse> GetTenantListAsync([FromQuery] TenantSearchDto dto)
        {
            var data = await tenantService.GetTenantListAsync(dto);
            return ResultHelper.Ok(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IAppResponse> UpdateTenantAsync([FromBody] TenantDto dto)
        {
            await tenantService.UpdateTenantAsync(dto);
            return ResultHelper.Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IAppResponse> DeleteTenantAsync(Guid id)
        {
            await tenantService.DeleteTenantAsync(id);
            return ResultHelper.Ok();
        }

        [HttpGet]
        public async Task<IAppResponse> GetDecryptInfoAsync(Guid tenantId, string type)
        {
            var data = await tenantService.GetDecryptInfoAsync(tenantId, type);
            return ResultHelper.Ok<string>(data);
        }
    }
}