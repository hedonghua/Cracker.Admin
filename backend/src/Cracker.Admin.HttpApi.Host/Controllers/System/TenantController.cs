using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.System
{
    [Route("api/[controller]/[action]")]
    public class TenantController : AdminController
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IAppResult> AddTenantAsync([FromBody] TenantDto dto)
        {
            await tenantService.AddTenantAsync(dto);
            return ResultHelper.Ok();
        }
    }
}