using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Organization
{
    public class EmployeeService : ApplicationService, IEmployeeService
    {
        private readonly IRepository<OrgEmployee> _employeeRepository;
        private readonly IRepository<OrgDeptEmployee> _deptEmployeeRepository;
        private readonly IRepository<OrgDept> orgDeptRepository;
        private readonly IRepository<OrgPosition> orgPositionRepository;

        public EmployeeService(IRepository<OrgEmployee> employeeRepository, IRepository<OrgDeptEmployee> deptEmployeeRepository,
            IRepository<OrgDept> orgDeptRepository, IRepository<OrgPosition> orgPositionRepository)
        {
            _employeeRepository = employeeRepository;
            _deptEmployeeRepository = deptEmployeeRepository;
            this.orgDeptRepository = orgDeptRepository;
            this.orgPositionRepository = orgPositionRepository;
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
            var entity = ObjectMapper.Map<EmployeeDto, OrgEmployee>(dto);
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
            var query = from e in await _employeeRepository.GetQueryableAsync()
                        join d in await orgDeptRepository.GetQueryableAsync() on e.DeptId equals d.Id into dd
                        from d2 in dd.DefaultIfEmpty()
                        join p in await orgPositionRepository.GetQueryableAsync() on e.PositionId equals p.Id into pp
                        from p2 in pp.DefaultIfEmpty()
                        orderby e.CreationTime descending
                        select new EmployeeListDto
                        {
                            Id = e.Id,
                            Code = e.Code,
                            Name = e.Name,
                            Sex = e.Sex,
                            Phone = e.Phone,
                            IdNo = e.IdNo,
                            FrontIdNoUrl = e.FrontIdNoUrl,
                            BackIdNoUrl = e.BackIdNoUrl,
                            BirthDay = e.BirthDay,
                            Address = e.Address,
                            Email = e.Email,
                            InTime = e.InTime,
                            OutTime = e.OutTime,
                            IsOut = e.IsOut,
                            UserId = e.UserId,
                            DeptId = e.DeptId,
                            PositionId = e.PositionId,
                            DeptName = d2.Name,
                            PositionName = p2.Name,
                        };
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x => x.Code.Contains(dto.Keyword) || x.Name.Contains(dto.Keyword) || x.Phone.Contains(dto.Keyword));
            }
            if (dto.DeptId.HasValue)
            {
                query = query.Where(x => x.Id == dto.DeptId);
            }
            var total = await query.CountAsync();
            var list = await query.ToListAsync();

            return new PagedResultStruct<EmployeeListDto>(dto) { Items = list, TotalCount = total };
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto.Id);
            var entity = await _employeeRepository.GetAsync(x => x.Id == dto.Id.Value);
            ReflectionHelper.AssignTo(dto, entity, "Id");
            await _employeeRepository.UpdateAsync(entity);
            //暂无多部门，直接写主部门
            var data = await _deptEmployeeRepository.SingleOrDefaultAsync(x => x.EmployeeId == dto.Id && x.IsMain);
            if (data != null)
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