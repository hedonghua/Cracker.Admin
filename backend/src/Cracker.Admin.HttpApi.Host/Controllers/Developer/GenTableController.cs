using System;
using System.Collections.Generic;
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
    public class GenTableController : AbpControllerBase
    {
        private readonly IGenTableService genTableService;

        public GenTableController(IGenTableService genTableService)
        {
            this.genTableService = genTableService;
        }

        [HttpGet]
        public async Task<IAppResult> GetDatabaseTableListAsync([FromQuery] DatabaseableSearchDto dto)
        {
            var data = await genTableService.GetDatabaseTableListAsync(dto);
            return ResultHelper.Ok(data);
        }

        [HttpGet]
        public async Task<IAppResult> GetGenTableListAsync([FromQuery] GenTableSearchDto dto)
        {
            var data = await genTableService.GetGenTableListAsync(dto);
            return ResultHelper.Ok(data);
        }

        [HttpPost]
        public async Task<IAppResult> AddGenTableAsync([FromBody] GenTableDto dto)
        {
            await genTableService.AddGenTableAsync(dto);
            return ResultHelper.Ok();
        }

        [HttpPut]
        public async Task<IAppResult> UpdateGenTableAsync([FromBody] GenTableDto dto)
        {
            await genTableService.UpdateGenTableAsync(dto);
            return ResultHelper.Ok();
        }

        [HttpDelete]
        public async Task<IAppResult> DeleteGenTableAsync([FromBody] List<Guid> genTableIds)
        {
            await genTableService.DeleteGenTableAsync(genTableIds);
            return ResultHelper.Ok();
        }

        /// <summary>
        /// 预览代码
        /// </summary>
        /// <param name="genTableId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IAppResult> PreviewCodeAsync(Guid genTableId)
        {
            var data = await genTableService.PreviewCodeAsync(genTableId);
            return ResultHelper.Ok(data);
        }
    }
}