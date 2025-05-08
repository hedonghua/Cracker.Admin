using Cracker.Admin.Entities;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;
using Cracker.Admin.Organization.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.Organization
{
    public class PositionService : ApplicationService, IPositionService
    {
        private readonly IRepository<OrgPosition> _positionRepository;
        private readonly IRepository<OrgPositionGroup> _positionGroupRepository;
        private readonly IRepository<OrgEmployee> _employeeRepository;

        public PositionService(IRepository<OrgPosition> positionRepository, IRepository<OrgPositionGroup> positionGroupRepository
            , IRepository<OrgEmployee> employeeRepository)
        {
            _positionRepository = positionRepository;
            _positionGroupRepository = positionGroupRepository;
            _employeeRepository = employeeRepository;
        }

        private async Task<List<PosistionLayerNames>> GetPosistionGroupNameAsync(List<Guid> ids)
        {
            var positions = (await _positionRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id)).Select(x => new { x.Id, x.GroupId }).ToList();
            var groups = await _positionGroupRepository.GetListAsync();
            var list = new List<PosistionLayerNames>();

            foreach (var item in positions)
            {
                var single = new PosistionLayerNames
                {
                    Id = item.Id
                };
                var allGroups = groups.Where(x => x.Id == item.GroupId).Select(x => x.ParentIds + "," + x.Id);
                foreach (var groupIds in allGroups)
                {
                    foreach (var groupId in groupIds.Split(","))
                    {
                        single.LayerName += groups.Find(x => x.Id.ToString() == groupId)?.GroupName + "/";
                    }
                }
                single.LayerName = single.LayerName?.Trim('/');

                list.Add(single);
            }

            return list;
        }

        public async Task<bool> AddPositionAsync(PositionDto dto)
        {
            var entity = ObjectMapper.Map<PositionDto, OrgPosition>(dto);
            entity.Code = DateTime.Now.Ticks.ToString();
            await _positionRepository.InsertAsync(entity);
            return true;
        }

        public async Task<bool> DeletePositionAsync(Guid id)
        {
            var hasEmployees = await _employeeRepository.AnyAsync(x => x.PositionId == id);
            if (hasEmployees) throw new BusinessException(message: "职位正在使用，不能删除");
            await _positionRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<PagedResultDto<PositionListDto>> GetPositionListAsync(PositionQueryDto dto)
        {
            var query = (await _positionRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.Keyword), x => x.Name.Contains(dto.Keyword!) || x.Code.Contains(dto.Keyword!))
                .WhereIf(dto.Level > 0, x => x.Level == dto.Level)
                .WhereIf(dto.Status > 0, x => x.Status == dto.Status)
                .WhereIf(dto.GroupId.HasValue, x => x.GroupId == dto.GroupId);
            var count = query.Count();
            var rows = query.Skip((dto.Current - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            var ids = rows.Select(x => x.Id).ToList();
            var list = ObjectMapper.Map<List<OrgPosition>, List<PositionListDto>>(rows);
            var names = await this.GetPosistionGroupNameAsync(ids);
            foreach (var item in list)
            {
                var tmp = names.FirstOrDefault(x => x.Id == item.Id);
                item.LayerName = tmp?.LayerName;
            }
            return new PagedResultDto<PositionListDto>(count, list);
        }

        public async Task<bool> UpdatePositionAsync(PositionDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));
            var entity = await _positionRepository.FindAsync(x => x.Id == dto.Id)
                ?? throw new AbpValidationException("数据不存在");
            entity.Name = dto.Name;
            entity.Level = dto.Level;
            entity.Status = dto.Status;
            entity.Description = dto.Description;
            entity.GroupId = dto.GroupId;
            await _positionRepository.UpdateAsync(entity, true);
            return true;
        }

        public async Task<List<AppOptionTree>> GetPositionTreeOptionAsync()
        {
            var groups = await _positionGroupRepository.GetListAsync();
            var positions = await _positionRepository.GetListAsync();
            var topGroups = groups.Where(x => !x.ParentId.HasValue).ToList();
            var list = new List<AppOptionTree>();
            List<AppOptionTree> GetChildren(string id)
            {
                var items = groups.Where(x => x.ParentId.ToString() == id);
                var children = new List<AppOptionTree>();
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        var t = new AppOptionTree()
                        {
                            Label = item.GroupName,
                            Value = item.Id.ToString()
                        };
                        t.Children = GetChildren(t.Value);
                        children.Add(t);
                        //最底级查职位
                        if (t.Children.Count == 0)
                        {
                            t.Children = positions.Where(x => x.GroupId.ToString() == t.Value).Select(x => new AppOptionTree
                            {
                                Label = x.Name,
                                Value = x.Id.ToString()
                            }).ToList();
                        }
                    }
                }
                else
                {
                    children = positions.Where(x => x.GroupId.ToString() == id).Select(x => new AppOptionTree
                    {
                        Label = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }
                return children;
            }

            foreach (var group in topGroups)
            {
                var t = new AppOptionTree()
                {
                    Label = group.GroupName,
                    Value = group.Id.ToString()
                };
                t.Children = GetChildren(t.Value);
                list.Add(t);
            }
            return list;
        }
    }
}