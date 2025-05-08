using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.Organization;
using Cracker.Admin.Organization.Dtos;

using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Cracker.Admin.Controllers.Organization
{
    [Authorize]
    [ApiController]
    [Route("api/position")]
    public class PositionController : AbpControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        /// <summary>
        /// 新增职位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("Org.Position.Add")]
        public async Task<AppResponse> AddPositionAsync([FromBody] PositionDto dto)
        {
            await _positionService.AddPositionAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 职位分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("Org.Position.List")]
        public async Task<AppResponse<PagedResultDto<PositionListDto>>> GetPositionListAsync([FromQuery] PositionQueryDto dto)
        {
            var data = await _positionService.GetPositionListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("Org.Position.Update")]
        public async Task<AppResponse> UpdatePositionAsync([FromBody] PositionDto dto)
        {
            await _positionService.UpdatePositionAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [HasPermission("Org.Position.Delete")]
        public async Task<AppResponse> DeletePositionAsync(Guid id)
        {
            await _positionService.DeletePositionAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 职位分组+职位树
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        public async Task<AppResponse<List<AppOptionTree>>> GetPositionTreeOptionAsync()
        {
            var data = await _positionService.GetPositionTreeOptionAsync();
            return AppResponse.Data(data);
        }
    }
}