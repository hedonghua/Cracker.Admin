using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Cracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.Organization;
using Cracker.Admin.Organization.Dtos;

namespace Cracker.Admin.Controllers.Organization
{
    [Route("api/dept")]
    public class DeptController : AdminController
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
        [HasPermission("admin_system_dept_add")]
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
        [HasPermission("admin_system_dept_list")]
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
        [HasPermission("admin_system_dept_update")]
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
        [HasPermission("admin_system_dept_delete")]
        public async Task<AppResponse> DeleteDeptAsync(Guid id)
        {
            await _deptService.DeleteDeptAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 获取部门组成的选项树
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        public async Task<AppResponse<List<AppOption>>> GetDeptOptionsAsync()
        {
            var data = await _deptService.GetDeptOptionsAsync();
            return AppResponse.Data(data);
        }
    }
}