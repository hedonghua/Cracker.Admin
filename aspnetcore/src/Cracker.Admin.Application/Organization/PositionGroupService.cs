using Cracker.Admin.Entities;
using Cracker.Admin.Models;
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
    public class PositionGroupService : ApplicationService, IPositionGroupService
    {
        private readonly IRepository<OrgPositionGroup> _positionGroupRepository;
        private readonly IRepository<OrgPosition> _positionRepository;

        public PositionGroupService(IRepository<OrgPositionGroup> positionGroupRepository, IRepository<OrgPosition> positionRepository)
        {
            _positionGroupRepository = positionGroupRepository;
            _positionRepository = positionRepository;
        }

        public async Task<bool> AddPositionGroupAsync(PositionGroupDto dto)
        {
            var entity = ObjectMapper.Map<PositionGroupDto, OrgPositionGroup>(dto);
            entity.ParentId = dto.ParentId;
            if (entity.ParentId.HasValue)
            {
                var all = await _positionGroupRepository.GetListAsync();
                entity.ParentIds = GetParentIds(all, entity.ParentId.Value);
            }
            await _positionGroupRepository.InsertAsync(entity);
            return true;
        }

        public string GetParentIds(List<OrgPositionGroup> all, Guid id)
        {
            var parentId = all.Find(x => x.Id == id)?.ParentId;
            if (parentId == null) return id.ToString();
            return GetParentIds(all, parentId.Value) + "," + id;
        }

        public async Task<bool> DeletePositionGroupAsync(Guid id)
        {
            var hasPositions = await _positionRepository.AnyAsync(x => x.GroupId == id);
            if (hasPositions)
            {
                throw new BusinessException(message: "分组下有职位，不能删除");
            }
            await _positionGroupRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<List<PositionGroupListDto>> GetPositionGroupListAsync(PositionGroupQueryDto dto)
        {
            if (!string.IsNullOrEmpty(dto.GroupName))
            {
                var filter = (await _positionGroupRepository.GetQueryableAsync()).Where(x => x.GroupName.Contains(dto.GroupName)).OrderBy(x => x.GroupName).ToList();
                return ObjectMapper.Map<List<OrgPositionGroup>, List<PositionGroupListDto>>(filter);
            }
            var all = (await _positionGroupRepository.GetQueryableAsync()).OrderBy(x => x.ParentIds).ToList();
            var top = all.Where(x => x.ParentId == null).ToList();
            var tree = new List<PositionGroupListDto>();
            var dict = new Dictionary<Guid, PositionGroupListDto>();

            foreach (var group in all)
            {
                var node = ObjectMapper.Map<OrgPositionGroup, PositionGroupListDto>(group);
                dict[node.Id] = node;

                if (group.ParentId == null)
                {
                    tree.Add(node);
                }
                else if (dict.TryGetValue(group.ParentId.Value, out var parentNode) && parentNode != null)
                {
                    parentNode.Children ??= [];
                    parentNode.Children.Add(node);
                    parentNode.Children = [.. parentNode.Children.OrderBy(x => x.Sort)];
                }
            }

            return [.. tree.OrderBy(t => t.Sort)];
        }

        public async Task<List<AppOption>> GetPositionGroupOptionsAsync()
        {
            return [.. (await _positionGroupRepository.GetQueryableAsync()).Select(x => new AppOption
            {
                Label = x.GroupName,
                Value = x.Id.ToString(),
                Extra = x.ParentId
            })];
        }

        public async Task<bool> UpdatePositionGroupAsync(PositionGroupDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));
            var entity = await _positionGroupRepository.GetAsync(x => x.Id == dto.Id);
            if(dto.ParentId == entity.Id)
            {
                throw new BusinessException(message: "不能选择自己为父级");
            }

            entity.GroupName = dto.GroupName;
            entity.Remark = dto.Remark;
            entity.ParentId = dto.ParentId;
            entity.Sort = dto.Sort;
            if (entity.ParentId.HasValue)
            {
                var all = await _positionGroupRepository.GetListAsync();
                entity.ParentIds = GetParentIds(all, entity.ParentId.Value);
            }
            await _positionGroupRepository.UpdateAsync(entity, true);
            return true;
        }
    }
}