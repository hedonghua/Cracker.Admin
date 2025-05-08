using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;
using Cracker.Admin.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Organization
{
    public class EmployeeService : ApplicationService, IEmployeeService
    {
        private readonly IRepository<OrgEmployee> _employeeRepository;
        private readonly IRepository<OrgDept> orgDeptRepository;
        private readonly IRepository<OrgPosition> orgPositionRepository;
        private readonly IdentityDomainService identityDomainService;

        public EmployeeService(IRepository<OrgEmployee> employeeRepository, IRepository<OrgDept> orgDeptRepository, IRepository<OrgPosition> orgPositionRepository
            , IdentityDomainService identityDomainService)
        {
            _employeeRepository = employeeRepository;
            this.orgDeptRepository = orgDeptRepository;
            this.orgPositionRepository = orgPositionRepository;
            this.identityDomainService = identityDomainService;
        }

        /// <summary>
        /// 生成工号
        /// </summary>
        /// <returns></returns>
        public async Task<string> GenerateCode()
        {
            var code = StringHelper.RandomStr(5, true);
            var exist = await _employeeRepository.AnyAsync(x => x.Code.ToLower() == code.ToLower());
            if (!exist) return code;
            return await GenerateCode();
        }

        public async Task<bool> AddEmployeeAsync(EmployeeDto dto)
        {
            if (await _employeeRepository.AnyAsync(x => x.Code.ToLower() == dto.Code.ToLower()))
            {
                throw new BusinessException(message: "工号已存在");
            }

            var entity = ObjectMapper.Map<EmployeeDto, OrgEmployee>(dto);
            await _employeeRepository.InsertAsync(entity);
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<PagedResultStruct<EmployeeListDto>> GetEmployeeListAsync(EmployeeQueryDto dto)
        {
            var powerData = await identityDomainService.GetPowerData();
            var query = from e in await _employeeRepository.GetQueryableAsync()
                        join d in await orgDeptRepository.GetQueryableAsync() on e.DeptId equals d.Id into dd
                        from d2 in dd.DefaultIfEmpty()
                        join p in await orgPositionRepository.GetQueryableAsync() on e.PositionId equals p.Id into pp
                        from p2 in pp.DefaultIfEmpty()
                        where powerData.DeptIds.Contains(e.DeptId) && powerData.EmployeeIds.Contains(e.Id)
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
                            Status = e.Status,
                            UserId = e.UserId,
                            DeptId = e.DeptId,
                            PositionId = e.PositionId,
                            DeptName = d2.Name,
                            PositionName = p2.Name,
                        };
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x => x.Code!.Contains(dto.Keyword) || x.Name!.Contains(dto.Keyword) || x.Phone!.Contains(dto.Keyword));
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
            if (entity.Code.ToLower() != dto.Code.ToLower() && await _employeeRepository.AnyAsync(x => x.Code.ToLower() == dto.Code.ToLower()))
            {
                throw new BusinessException(message: "工号已存在");
            }

            ReflectionHelper.AssignTo(dto, entity, "Id");
            await _employeeRepository.UpdateAsync(entity);
            return true;
        }

        public async Task EmployeeBindUserAsync(EmployeeBindUserDto dto)
        {
            var employee = await _employeeRepository.GetAsync(x => x.Id == dto.EmployeeId);
            employee.UserId = dto.UserId;
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}