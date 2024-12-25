using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mapster;

using Cracker.Admin.Entity;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;
using Cracker.Admin.System.Dtos;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Organization
{
    public class DeptService : ApplicationService, IDeptService
    {
        private readonly IRepository<OrgDept> _deptRepository;
        private readonly IRepository<OrgDeptEmployee> _employeeRepository;

        public DeptService(IRepository<OrgDept> deptRepository, IRepository<OrgDeptEmployee> employeeRepository)
        {
            _deptRepository = deptRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddDeptAsync(DeptDto dto)
        {
            var entity = dto.Adapt<OrgDept>();
            entity.ParentId = dto.ParentId;
            entity.Code = DateTime.Now.Ticks.ToString();
            if (entity.ParentId.HasValue)
            {
                var all = await _deptRepository.GetListAsync();
                int layer = 1;
                entity.ParentIds = GetParentIds(all, entity.ParentId.Value, ref layer);
                entity.Layer = layer;
            }
            await _deptRepository.InsertAsync(entity);
            return true;
        }

        public string GetParentIds(List<OrgDept> all, Guid id, ref int layer)
        {
            layer += 1;
            var parentId = all.Find(x => x.Id == id)?.ParentId;
            if (parentId == null) return id.ToString();
            return GetParentIds(all, parentId.Value, ref layer) + "," + id;
        }

        public async Task<bool> DeleteDeptAsync(Guid id)
        {
            var hasEmployees = await _employeeRepository.AnyAsync(x => x.DeptId == id);
            if (hasEmployees) throw new BusinessException("-1", "部门下存在员工，不能删除");
            await _deptRepository.DeleteAsync(x => id == x.Id);
            return true;
        }

        public async Task<List<DeptListDto>> GetDeptListAsync(DeptQueryDto dto)
        {
            var query = (await _deptRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.Name), x => x.Name.Contains(dto.Name!));
            var rows = query.ToList();
            return ObjectMapper.Map<List<OrgDept>, List<DeptListDto>>(rows);
        }

        public async Task<List<AppOption>> GetDeptOptionsAsync()
        {
            return [.. (await _deptRepository.GetQueryableAsync()).Select(x => new AppOption
            {
                Label = x.Name,
                Value = x.Id.ToString(),
                Extra = x.ParentId
            })];
        }

        public async Task<bool> UpdateDeptAsync(DeptDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));
            var entity = await _deptRepository.GetAsync(x => x.Id == dto.Id);
            entity.Name = dto.Name;
            entity.Sort = dto.Sort;
            entity.Sort = dto.Sort;
            entity.Description = dto.Description;
            entity.Status = dto.Status;
            entity.CuratorId = dto.CuratorId;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
            entity.ParentId = dto.ParentId;
            if (entity.ParentId.HasValue)
            {
                var all = await _deptRepository.GetListAsync();
                int layer = 1;
                entity.ParentIds = GetParentIds(all, entity.ParentId.Value, ref layer);
                entity.Layer = layer;
            }
            await _deptRepository.UpdateAsync(entity, true);
            return true;
        }
    }
}