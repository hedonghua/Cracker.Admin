using System;
using System.Threading.Tasks;

using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;

namespace Cracker.Admin.Organization
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddEmployeeAsync(EmployeeDto dto);

        /// <summary>
        /// 员工树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultStruct<EmployeeListDto>> GetEmployeeListAsync(EmployeeQueryDto dto);

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateEmployeeAsync(EmployeeDto dto);

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteEmployeeAsync(Guid id);

        /// <summary>
        /// 生成工号
        /// </summary>
        /// <returns></returns>
        Task<string> GenerateCode();

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task EmployeeBindUserAsync(EmployeeBindUserDto dto);
    }
}