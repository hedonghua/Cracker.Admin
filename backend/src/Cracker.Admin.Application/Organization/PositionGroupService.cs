using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mapster;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Cracker.Admin.Entities;

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
            var entity = dto.Adapt<OrgPositionGroup>();
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
                throw new BusinessException("-1", "分组下有职位，不能删除");
            }
            await _positionGroupRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<List<PositionGroupListDto>> GetPositionGroupListAsync(PositionGroupQueryDto dto)
        {
            var query = (await _positionGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.GroupName), x => x.GroupName.Contains(dto.GroupName!));
            var count = query.Count();
            var rows = query.ToList();
            return ObjectMapper.Map<List<OrgPositionGroup>, List<PositionGroupListDto>>(rows);
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
            var entity = await _positionGroupRepository.FindAsync(x => x.Id == dto.Id)
                ?? throw new AbpValidationException("数据不存在");
            entity.GroupName = dto.GroupName;
            entity.Remark = dto.Remark;
            entity.ParentId = dto.ParentId;
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