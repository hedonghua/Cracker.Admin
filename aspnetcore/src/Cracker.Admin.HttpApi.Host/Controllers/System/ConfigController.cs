using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Frozen;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : AbpControllerBase
    {
        private readonly ConfigDomainService configDomainService;

        public ConfigController(ConfigDomainService configDomainService)
        {
            this.configDomainService = configDomainService;
        }

        [HttpGet("get")]
        [HasPermission("Sys.Config.Read")]
        public async Task<AppResponse<string>> GetConfigValue(string key)
        {
            var value = await configDomainService.GetAsync(key);
            return A.Data(value);
        }

        [HttpPost("set")]
        [HasPermission("Sys.Config.Write")]
        public async Task<AppResponse> SetConfigValue([FromBody] ConfigDto dto)
        {
            await configDomainService.SetAsync(dto.Key!, dto.Value!);
            return A.Ok();
        }

        [HttpGet("group")]
        [HasPermission("Sys.Config.Read")]
        public async Task<AppResponse<FrozenDictionary<string, string>>> GetKeysAsync(string group)
        {
            var data = await configDomainService.GetKeysAsync(group);
            return A.Data(data);
        }
    }
}