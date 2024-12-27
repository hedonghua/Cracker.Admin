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
    [Route("api/position")]
    public class PositionController : AdminController
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
        [AppResultFilter]
        [HasPermission("admin_system_position_add")]
        public Task<bool> AddPositionAsync([FromBody] PositionDto dto) => _positionService.AddPositionAsync(dto);

        /// <summary>
        /// 职位分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [HasPermission("admin_system_position_list")]
        public Task<PagedResultDto<PositionListDto>> GetPositionListAsync([FromQuery] PositionQueryDto dto) => _positionService.GetPositionListAsync(dto);

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [HasPermission("admin_system_position_update")]
        public Task<bool> UpdatePositionAsync([FromBody] PositionDto dto) => _positionService.UpdatePositionAsync(dto);

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [AppResultFilter]
        [HasPermission("admin_system_position_delete")]
        public Task<bool> DeletePositionAsync(Guid id) => _positionService.DeletePositionAsync(id);

        /// <summary>
        /// 职位分组+职位树
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        [AppResultFilter]
        public Task<List<AppOptionTree>> GetPositionTreeOptionAsync() => _positionService.GetPositionTreeOptionAsync();
    }
}