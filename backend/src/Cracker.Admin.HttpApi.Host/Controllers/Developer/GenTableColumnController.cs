using System.Threading.Tasks;

using Cracker.Admin.Core;
using Cracker.Admin.Developer;
using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Developer
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class GenTableColumnController : AbpControllerBase
    {
        private readonly IGenTableColumnService genColumnService;

        public GenTableColumnController(IGenTableColumnService genColumnService)
        {
            this.genColumnService = genColumnService;
        }

        /// <summary>
        /// 查询列配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IAppResult> GetGenTableColumnListAsync([FromQuery] GenTableColumnSearchDto dto)
        {
            var data = await genColumnService.GetGenTableColumnListAsync(dto);
            return ResultHelper.Ok(data);
        }

        /// <summary>
        /// 保存列配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IAppResult> SaveGenTableColumnAsync([FromBody] GenTableColumnDto dto)
        {
            await genColumnService.SaveGenTableColumnAsync(dto);
            return ResultHelper.Ok();
        }
    }
}