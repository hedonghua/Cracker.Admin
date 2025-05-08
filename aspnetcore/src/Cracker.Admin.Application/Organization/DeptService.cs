using Cracker.Admin.Entities;
using Cracker.Admin.Organization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Organization
{
    public class DeptService : ApplicationService, IDeptService
    {
        private readonly IRepository<OrgDept> _deptRepository;
        private readonly IRepository<OrgEmployee> _employeeRepository;

        public DeptService(IRepository<OrgDept> deptRepository, IRepository<OrgEmployee> employeeRepository)
        {
            _deptRepository = deptRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddDeptAsync(DeptDto dto)
        {
            if (await _deptRepository.AnyAsync(x => x.Code.ToLower() == dto.Code!.ToLower()))
            {
                throw new BusinessException(message: "部门编号已存在");
            }

            var entity = ObjectMapper.Map<DeptDto, OrgDept>(dto);
            entity.ParentId = dto.ParentId;
            entity.Code = dto.Code;
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
            if (hasEmployees) throw new BusinessException(message: "部门下存在员工，不能删除");
            await _deptRepository.DeleteAsync(x => id == x.Id);
            return true;
        }

        public async Task<List<DeptListDto>> GetDeptListAsync(DeptQueryDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Name))
            {
                var filter = (await _deptRepository.GetQueryableAsync()).Where(x => x.Name.Contains(dto.Name)).OrderBy(x => x.Name).ToList();
                return ObjectMapper.Map<List<OrgDept>, List<DeptListDto>>(filter);
            }
            var all = (await _deptRepository.GetQueryableAsync()).OrderBy(x => x.ParentIds).ToList();
            var top = all.Where(x => x.ParentId == null).ToList();
            var tree = new List<DeptListDto>();
            var dict = new Dictionary<Guid, DeptListDto>();

            foreach (var dept in all)
            {
                var node = ObjectMapper.Map<OrgDept, DeptListDto>(dept);
                dict[node.Id] = node;

                if (dept.ParentId == null)
                {
                    tree.Add(node);
                }
                else if (dict.TryGetValue(dept.ParentId.Value, out var parentNode) && parentNode != null)
                {
                    parentNode.Children ??= [];
                    parentNode.Children.Add(node);
                    parentNode.Children = [.. parentNode.Children.OrderBy(x => x.Sort)];
                }
            }

            return [.. tree.OrderBy(t => t.Sort)];
        }

        public async Task<bool> UpdateDeptAsync(DeptDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));

            var entity = await _deptRepository.GetAsync(x => x.Id == dto.Id);
            if (!entity.Code.Equals(dto.Code, StringComparison.CurrentCultureIgnoreCase) && await _deptRepository.AnyAsync(x => x.Code.ToLower() == dto.Code!.ToLower()))
            {
                throw new BusinessException(message: "部门编号已存在");
            }
            if (dto.ParentId == entity.Id)
            {
                throw new BusinessException(message: "不能选择自己为父级");
            }

            entity.Name = dto.Name;
            entity.Code = dto.Code;
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