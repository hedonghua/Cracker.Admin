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
    [Route("api/dept")]
    public class DeptController : AbpControllerBase
    {
        private readonly IDeptService _deptService;

        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("Org.Dept.Add")]
        public async Task<AppResponse> AddDeptAsync([FromBody] DeptDto dto)
        {
            await _deptService.AddDeptAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 部门树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("Org.Dept.List")]
        public async Task<AppResponse<List<DeptListDto>>> GetDeptListAsync([FromQuery] DeptQueryDto dto)
        {
            var data = await _deptService.GetDeptListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("Org.Dept.Update")]
        public async Task<AppResponse> UpdateDeptAsync([FromBody] DeptDto dto)
        {
            await _deptService.UpdateDeptAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [HasPermission("Org.Dept.Delete")]
        public async Task<AppResponse> DeleteDeptAsync(Guid id)
        {
            await _deptService.DeleteDeptAsync(id);
            return AppResponse.Ok();
        }
    }
}