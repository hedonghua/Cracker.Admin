using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.MyExceptions;
using Cracker.Admin.System.Dtos;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.System
{
    public class MenuService : ApplicationService, IMenuService
    {
        private readonly IRepository<SysMenu> _menuRepository;

        public MenuService(IRepository<SysMenu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<bool> AddMenuAsync(MenuDto dto)
        {
            if (dto.FunctionType == (int)FunctionType.Menu && string.IsNullOrWhiteSpace(dto.Path))
            {
                throw new TipException("菜单的路由不能为空");
            }
            var isExist = await _menuRepository.AnyAsync(x => x.Path != null && dto.Path != null && x.Path.ToLower() == dto.Path.ToLower());
            if (isExist)
            {
                throw new TipException($"已存在【{dto.Path}】菜单路由");
            }
            var entity = ObjectMapper.Map<MenuDto, SysMenu>(dto);
            await _menuRepository.InsertAsync(entity, true);
            RedisExtensionHelper.RemoveByPattern(UserCacheHelper.UserInfoKeyPattern);
            return true;
        }

        public async Task<bool> DeleteMenusAsync(Guid[] ids)
        {
            await _menuRepository.DeleteAsync(x => ids.Contains(x.Id));
            return true;
        }

        public async Task<List<MenuListDto>> GetMenuListAsync(MenuQueryDto dto)
        {
            //是否过滤
            var isFilter = !string.IsNullOrEmpty(dto.Title) || !string.IsNullOrEmpty(dto.Path);
            var all = (await _menuRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.Title), x => !string.IsNullOrEmpty(x.Title) && x.Title.Contains(dto.Title!))
                .WhereIf(!string.IsNullOrEmpty(dto.Path), x => !string.IsNullOrEmpty(x.Path) && x.Path.Contains(dto.Path!))
                .ToList();
            var top = all.Where(x => isFilter || !x.ParentId.HasValue || x.ParentId == Guid.Empty).OrderBy(x => x.Sort).ToList();
            var topMap = ObjectMapper.Map<List<SysMenu>, List<MenuListDto>>(top);
            if (isFilter) return topMap;
            foreach (var item in topMap)
            {
                item.Children = getChildren(item.Id);
            }

            List<MenuListDto> getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                var childrenMap = ObjectMapper.Map<List<SysMenu>, List<MenuListDto>>(children);
                foreach (var item in childrenMap)
                {
                    item.Children = getChildren(item.Id);
                }
                return childrenMap;
            }

            return topMap;
        }

        public async Task<List<AppOptionTree>> GetMenuOptionsAsync()
        {
            var all = (await _menuRepository.GetQueryableAsync()).ToList();
            var top = all.Where(x => !x.ParentId.HasValue || x.ParentId == Guid.Empty && x.FunctionType == FunctionType.Menu).ToList();
            var topMap = new List<AppOptionTree>();
            foreach (var item in top)
            {
                topMap.Add(new AppOptionTree
                {
                    Label = item.Title,
                    Value = item.Id.ToString(),
                    Children = getChildren(item.Id),
                    Extra = new { Type = (int)item.FunctionType }
                });
            }

            List<AppOptionTree> getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).ToList();
                var childrenMap = new List<AppOptionTree>();
                foreach (var item in children)
                {
                    childrenMap.Add(new AppOptionTree
                    {
                        Label = item.Title,
                        Value = item.Id.ToString(),
                        Children = getChildren(item.Id),
                        Extra = new { Type = (int)item.FunctionType }
                    });
                }
                return childrenMap;
            }

            return topMap;
        }

        public async Task<bool> UpdateMenuAsync(MenuDto dto)
        {
            if (dto.FunctionType == (int)FunctionType.Menu && string.IsNullOrWhiteSpace(dto.Path))
            {
                throw new Exception("菜单的路由不能为空");
            }
            var isExist = await _menuRepository.AnyAsync(x => x.Path != null && dto.Path != null && x.Path.ToLower() == dto.Path.ToLower());
            var entity = await _menuRepository.FindAsync(x => x.Id == dto.Id) ?? throw new AbpValidationException("数据不存在");
            if (isExist && entity.Path != null && dto.Path!.ToLower() != entity.Path.ToLower())
            {
                throw new TipException($"已存在【{dto.Path}】菜单路由");
            }
            entity.Title = dto.Title;
            entity.Name = dto.Name;
            entity.Icon = dto.Icon;
            entity.Path = dto.Path;
            entity.FunctionType = (FunctionType)dto.FunctionType;
            entity.Permission = dto.Permission;
            entity.ParentId = dto.ParentId;
            entity.Sort = dto.Sort;
            entity.Hidden = dto.Hidden;
            await _menuRepository.UpdateAsync(entity, true);
            return true;
        }
    }
}