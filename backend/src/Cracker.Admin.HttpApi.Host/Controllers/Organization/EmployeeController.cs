using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.Models;
using Cracker.Admin.Organization;
using Cracker.Admin.Organization.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.Organization
{
    [Route("api/employee")]
    public class EmployeeController : AdminController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [HasPermission("admin_system_employee_add")] public async Task<AppResponse> AddEmployeeAsync([FromBody] EmployeeDto dto)
        {
            await _employeeService.AddEmployeeAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 员工列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_employee_list")]
        public async Task<AppResponse<PagedResultStruct<EmployeeListDto>>> GetEmployeeListAsync([FromQuery] EmployeeQueryDto dto)
        {
            var data = await _employeeService.GetEmployeeListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [HasPermission("admin_system_employee_update")]
        public async Task<AppResponse> UpdateEmployeeAsync([FromBody] EmployeeDto dto)
        {
            await _employeeService.UpdateEmployeeAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        [HasPermission("admin_system_employee_delete")]
        public async Task<AppResponse> DeleteEmployeeAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return AppResponse.Ok();
        }
    }
}