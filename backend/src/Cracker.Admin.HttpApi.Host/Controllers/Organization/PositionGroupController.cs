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
        [AppResultFilter]
        [HasPermission("admin_system_positiongroup_add")]
        public Task<bool> AddPositionGroupAsync([FromBody] PositionGroupDto dto) => _positionGroupService.AddPositionGroupAsync(dto);

        /// <summary>
        /// 职位分组分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [HasPermission("admin_system_positiongroup_list")]
        public Task<List<PositionGroupListDto>> GetPositionGroupListAsync([FromQuery] PositionGroupQueryDto dto) => _positionGroupService.GetPositionGroupListAsync(dto);

        /// <summary>
        /// 修改职位分组
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [HasPermission("admin_system_positiongroup_update")]
        public Task<bool> UpdatePositionGroupAsync([FromBody] PositionGroupDto dto) => _positionGroupService.UpdatePositionGroupAsync(dto);

        /// <summary>
        /// 删除职位分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [AppResultFilter]
        [HasPermission("admin_system_positiongroup_delete")]
        public Task<bool> DeletePositionGroupAsync(Guid id) => _positionGroupService.DeletePositionGroupAsync(id);

        /// <summary>
        /// 获取职位分组
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        [AppResultFilter]
        public Task<List<AppOption>> GetPositionGroupOptionsAsync() => _positionGroupService.GetPositionGroupOptionsAsync();
    }
}