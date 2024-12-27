using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Dapper;

using Mapster;
using Cracker.Admin.Models;
using Cracker.Admin.Organization.Dtos;
using Cracker.Admin.Organization.Models;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Cracker.Admin.Entities;

namespace Cracker.Admin.Organization
{
    public class PositionService : ApplicationService, IPositionService
    {
        private readonly IRepository<OrgPosition> _positionRepository;
        private readonly IDbConnection _connection;
        private readonly IRepository<OrgPositionGroup> _positionGroupRepository;
        private readonly IRepository<OrgDeptEmployee> _employeeRepository;

        public PositionService(IRepository<OrgPosition> positionRepository, IDbConnection connection, IRepository<OrgPositionGroup> positionGroupRepository
            , IRepository<OrgDeptEmployee> employeeRepository)
        {
            _positionRepository = positionRepository;
            _connection = connection;
            _positionGroupRepository = positionGroupRepository;
            _employeeRepository = employeeRepository;
        }

        private async Task<List<PosistionLayerNames>> GetPosistionGroupNameAsync(List<Guid> ids)
        {
            if (ids.Count == 0) return [];
            var sql = @"SELECT
	                        p.id as Id,
	                        GROUP_CONCAT( g2.group_name SEPARATOR '/' ) AS LayerName
                        FROM
	                        org_position p
	                        INNER JOIN org_position_group g1 ON p.group_id = g1.id
	                        INNER JOIN org_position_group g2 ON FIND_IN_SET( g2.id, CONCAT( g1.parent_ids, ',', g1.id ) )
                        where p.id in @ids
                        GROUP BY
	                        p.id";
            return (await _connection.QueryAsync<PosistionLayerNames>(sql, new { ids })).ToList();
        }

        public async Task<bool> AddPositionAsync(PositionDto dto)
        {
            var entity = dto.Adapt<OrgPosition>();
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
                .WhereIf(!string.IsNullOrEmpty(dto.Name), x => x.Name.Contains(dto.Name!))
                .WhereIf(!string.IsNullOrEmpty(dto.Code), x => x.Code == dto.Code)
                .WhereIf(dto.Level > 0, x => x.Level == dto.Level)
                .WhereIf(dto.Status > 0, x => x.Status == dto.Status)
                .WhereIf(dto.GroupId.HasValue, x => x.GroupId == dto.GroupId);
            var count = query.Count();
            var rows = query.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
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