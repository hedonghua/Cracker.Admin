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

namespace Cracker.Admin.Controllers.Organization
{
    [Route("api/position-group")]
    public class PositionGroupController : AdminController
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
        [HasPermission("admin_system_positiongroup_add")]
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
        [HasPermission("admin_system_positiongroup_list")]
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
        [HasPermission("admin_system_positiongroup_update")]
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
        [HasPermission("admin_system_positiongroup_delete")]
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