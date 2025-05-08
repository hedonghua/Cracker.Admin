using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.Organization;
using Cracker.Admin.Organization.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Organization
{
    [Authorize]
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : AbpControllerBase
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
        [HasPermission("Org.Employee.Add")]
        public async Task<AppResponse> AddEmployeeAsync([FromBody] EmployeeDto dto)
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
        [HasPermission("Org.Employee.List")]
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
        [HasPermission("Org.Employee.Update")]
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
        [HasPermission("Org.Employee.Delete")]
        public async Task<AppResponse> DeleteEmployeeAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 生成工号
        /// </summary>
        /// <returns></returns>
        [HttpGet("gencode")]
        public async Task<AppResponse<string>> GenerateCode()
        {
            var code = await _employeeService.GenerateCode();
            return A.Data(code);
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("bind-user")]
        [HasPermission("Org.Employee.BindUser")]
        public async Task<AppResponse> EmployeeBindUserAsync([FromBody] EmployeeBindUserDto dto)
        {
            await _employeeService.EmployeeBindUserAsync(dto);
            return A.Ok();
        }
    }
}