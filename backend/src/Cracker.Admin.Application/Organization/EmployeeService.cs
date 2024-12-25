using System;
using System.Data;
using System.Threading.Tasks;

using Mapster;

using Cracker.Admin.Entity;
using Cracker.Admin.Extension;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Organization
{
    public class EmployeeService : ApplicationService, IEmployeeService
    {
        private readonly IRepository<OrgEmployee> _employeeRepository;
        private readonly IDbConnection _connection;
        private readonly IRepository<OrgDeptEmployee> _deptEmployeeRepository;

        public EmployeeService(IRepository<OrgEmployee> employeeRepository, IDbConnection connection,IRepository<OrgDeptEmployee> deptEmployeeRepository)
        {
            _employeeRepository = employeeRepository;
            _connection = connection;
            _deptEmployeeRepository = deptEmployeeRepository;
        }

        /// <summary>
        /// 生成工号
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateCode()
        {
            var code = StringHelper.RandomStr(5, true);
            var exist = await _employeeRepository.AnyAsync(x => x.Code == code);
            if (!exist) return code;
            return await GenerateCode();
        }

        public async Task<bool> AddEmployeeAsync(EmployeeDto dto)
        {
            var entity = dto.Adapt<OrgEmployee>();
            entity.Code = await this.GenerateCode();
            var rs = await _employeeRepository.InsertAsync(entity);
            //暂无多部门，直接写主部门
            await _deptEmployeeRepository.InsertAsync(new OrgDeptEmployee
            {
               DeptId = dto.DeptId,
               EmployeeId = rs.Id,
               IsMain = true,
               PositionId = dto.PositionId
            });
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<PagedResultStruct<EmployeeListDto>> GetEmployeeListAsync(EmployeeQueryDto dto)
        {
            var sql = @"SELECT
	                    e.id,
	                    e.`code`,
	                    e.`name`,
	                    e.sex,
	                    e.phone,
	                    e.idno,
	                    e.front_idno_url as FrontIdNoUrl,
	                    e.back_idno_url as BackIdNoUrl,
	                    e.birthday,
	                    e.address,
	                    e.email,
	                    e.in_time as InTime,
	                    e.out_time as OutTime,
	                    e.is_out as IsOut,
	                    e.user_id as UserId,
	                    e.dept_id as DeptId,
	                    e.position_id as PositionId,
	                    d.`name` AS DeptName,
	                    p.`name` AS PositionName
                    FROM
	                    org_employee AS e
	                    LEFT JOIN org_dept d ON e.dept_id = d.id
	                    LEFT JOIN org_position p ON e.position_id = p.id where e.is_deleted=0 ";
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                sql += " and (e.code like @Keyword or e.name like @Keyword or e.phone like @Keyword) ";
                dto.Keyword = string.Concat("%", dto.Keyword, "%");
            }
            if (dto.DeptId.HasValue)
            {
                sql += " and e.dept_id = @DeptId ";
            }
            sql += " order by e.creation_time desc ";
            return await _connection.GetPagedAsync<EmployeeListDto>(sql, dto.Page, dto.Size, dto);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.Id);
            var entity = await _employeeRepository.GetAsync(x => x.Id == dto.Id.Value);
            ReflectionHelper.AssignTo(dto, entity, "Id");
            await _employeeRepository.UpdateAsync(entity);
            //暂无多部门，直接写主部门
            var data = await _deptEmployeeRepository.SingleOrDefaultAsync(x=>x.EmployeeId == dto.Id && x.IsMain);
            if(data != null)
            {
                data.DeptId = dto.DeptId;
                data.PositionId = dto.PositionId;
                await _deptEmployeeRepository.UpdateAsync(data);
            }
            else
            {
                await _deptEmployeeRepository.InsertAsync(new OrgDeptEmployee
                {
                    DeptId = dto.DeptId,
                    EmployeeId = entity.Id,
                    IsMain = true,
                    PositionId = dto.PositionId
                });
            }
            return true;
        }
    }
}