using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using Cracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Cracker.Admin.CustomAttrs;
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
        [AppResultFilter]
        [Permission("admin_system_dept_add")]
        public Task<bool> AddDeptAsync([FromBody] DeptDto dto) => _deptService.AddDeptAsync(dto);

        /// <summary>
        /// 部门树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [Permission("admin_system_dept_list")]
        public Task<List<DeptListDto>> GetDeptListAsync([FromQuery] DeptQueryDto dto) => _deptService.GetDeptListAsync(dto);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [Permission("admin_system_dept_update")]
        public Task<bool> UpdateDeptAsync([FromBody] DeptDto dto) => _deptService.UpdateDeptAsync(dto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [AppResultFilter]
        [Permission("admin_system_dept_delete")]
        public Task<bool> DeleteDeptAsync(Guid id) => _deptService.DeleteDeptAsync(id);

        /// <summary>
        /// 获取部门组成的选项树
        /// </summary>
        /// <returns></returns>
        [HttpGet("options")]
        [AppResultFilter]
        public Task<List<AppOption>> GetDeptOptionsAsync() => _deptService.GetDeptOptionsAsync();
    }
}