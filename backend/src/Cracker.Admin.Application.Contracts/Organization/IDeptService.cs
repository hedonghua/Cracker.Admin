using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;
using Cracker.Admin.Organization.Dtos;

namespace Cracker.Admin.Organization
{
    public interface IDeptService : IApplicationService
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddDeptAsync(DeptDto dto);

        /// <summary>
        /// 部门树形列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<List<DeptListDto>> GetDeptListAsync(DeptQueryDto dto);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateDeptAsync(DeptDto dto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteDeptAsync(Guid id);

        /// <summary>
        /// 获取部门组成的选项树
        /// </summary>
        /// <returns></returns>
        Task<List<AppOption>> GetDeptOptionsAsync();
    }
}