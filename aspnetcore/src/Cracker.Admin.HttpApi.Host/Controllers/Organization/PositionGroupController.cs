using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.Organization;
using Cracker.Admin.Organization.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Organization
{
    [Authorize]
    [ApiController]
    [Route("api/position-group")]
    public class PositionGroupController : AbpControllerBase
    {
        private readonly IPositionGroupService _positionGroupService;

        public PositionGroupController(IPositionGroupService positionGroupService)
        {
            _positionGroupService = positionGroupService;
        }

        /// <summary>
        /// 新增职位分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("Org.PositionGroup.Add")]
        public async Task<AppResponse> AddPositionGroupAsync([FromBody] PositionGroupDto dto)
        {
            await _positionGroupService.AddPositionGroupAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 职位分组分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("Org.PositionGroup.List")]
        public async Task<AppResponse<List<PositionGroupListDto>>> GetPositionGroupListAsync([FromQuery] PositionGroupQueryDto dto)
        {
            var data = await _positionGroupService.GetPositionGroupListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改职位分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("Org.PositionGroup.Update")]
        public async Task<AppResponse> UpdatePositionGroupAsync([FromBody] PositionGroupDto dto)
        {
            await _positionGroupService.UpdatePositionGroupAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除职位分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [HasPermission("Org.PositionGroup.Delete")]
        public async Task<AppResponse> DeletePositionGroupAsync(Guid id)
        {
            await _positionGroupService.DeletePositionGroupAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 获取职位分组
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        public async Task<AppResponse<List<AppOption>>> GetPositionGroupOptionsAsync()
        {
            var data = await _positionGroupService.GetPositionGroupOptionsAsync();
            return AppResponse.Data(data);
        }
    }
}