using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Cracker.Admin.System.Dtos;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.System
{
    public class MenuService : ApplicationService, IMenuService
    {
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IdentityDomainService identityDomainService;

        public MenuService(IRepository<SysMenu> menuRepository, IdentityDomainService identityDomainService)
        {
            _menuRepository = menuRepository;
            this.identityDomainService = identityDomainService;
        }

        public async Task<bool> AddMenuAsync(MenuDto dto)
        {
            if (dto.FunctionType == (int)FunctionType.Menu && string.IsNullOrWhiteSpace(dto.Path))
            {
                throw new Exception("菜单的路由不能为空");
            }
            if (dto.ParentId.HasValue)
            {
                var parentMenu = await _menuRepository.GetAsync(x => x.ParentId == dto.ParentId);
                if (parentMenu.FunctionType == FunctionType.Button)
                {
                    throw new BusinessException(message: "菜单父级不能是按钮");
                }
            }
            var isExist = await _menuRepository.AnyAsync(x => x.Path != null && dto.Path != null && x.Path.ToLower() == dto.Path.ToLower());
            if (isExist)
            {
                throw new BusinessException(message: $"已存在【{dto.Path}】菜单路由");
            }
            if (!string.IsNullOrEmpty(dto.Name) && await _menuRepository.AnyAsync(x => x.Name != null && x.Name.ToLower() == dto.Name.ToLower()))
            {
                throw new BusinessException(message: "组件名已存在");
            }
            var entity = ObjectMapper.Map<MenuDto, SysMenu>(dto);
            await _menuRepository.InsertAsync(entity, true);
            await identityDomainService.DelAdminUserPermissionCacheAsync();
            return true;
        }

        public async Task<bool> DeleteMenusAsync(Guid[] ids)
        {
            await _menuRepository.DeleteAsync(x => ids.Contains(x.Id));
            foreach (var item in ids)
            {
                await identityDomainService.DelUserPermissionCacheByMenuIdAsync(item);
            }
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

            List<MenuListDto>? getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                if (children.Count == 0) return null;
                var childrenMap = ObjectMapper.Map<List<SysMenu>, List<MenuListDto>>(children);
                foreach (var item in childrenMap)
                {
                    item.Children = getChildren(item.Id);
                }
                return childrenMap;
            }

            return topMap;
        }

        public async Task<List<MenuOptionTreeDto>> GetMenuOptionsAsync(int type = 0)
        {
            var all = (await _menuRepository.GetQueryableAsync()).WhereIf(type > 0, x => (int)x.FunctionType == type).ToList();
            var top = all.Where(x => !x.ParentId.HasValue || x.ParentId == Guid.Empty && x.FunctionType == FunctionType.Menu)
                .OrderBy(x => x.Sort).ToList();
            var topMap = new List<MenuOptionTreeDto>();
            foreach (var item in top)
            {
                topMap.Add(new MenuOptionTreeDto
                {
                    Key = item.Id.ToString(),
                    Label = item.Title,
                    Value = item.Id.ToString(),
                    Children = getChildren(item.Id),
                    Extra = new { Type = (int)item.FunctionType }
                });
            }

            List<MenuOptionTreeDto>? getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                if (children.Count == 0) return null;
                var childrenMap = new List<MenuOptionTreeDto>();
                foreach (var item in children)
                {
                    childrenMap.Add(new MenuOptionTreeDto
                    {
                        Key = item.Id.ToString(),
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
            if (dto.ParentId.HasValue)
            {
                var parentMenu = await _menuRepository.GetAsync(x => x.ParentId == dto.ParentId);
                if (parentMenu.FunctionType == FunctionType.Button)
                {
                    throw new BusinessException(message: "菜单父级不能是按钮");
                }
            }
            var isExist = await _menuRepository.AnyAsync(x => x.Path != null && dto.Path != null && x.Path.ToLower() == dto.Path.ToLower());
            var entity = await _menuRepository.FindAsync(x => x.Id == dto.Id) ?? throw new AbpValidationException("数据不存在");
            if (isExist && entity.Path != null && dto.Path!.ToLower() != entity.Path.ToLower())
            {
                throw new BusinessException(message: $"已存在【{dto.Path}】菜单路由");
            }
            if (!string.IsNullOrEmpty(dto.Name) && (entity.Name != null && !entity.Name.Equals(dto.Name, StringComparison.CurrentCultureIgnoreCase))
                && await _menuRepository.AnyAsync(x => x.Name != null && x.Name.ToLower() == dto.Name.ToLower()))
            {
                throw new BusinessException(message: "组件名已存在");
            }
            if (dto.ParentId == entity.Id)
            {
                throw new BusinessException(message: "不能选择自己为父级");
            }

            bool updatePermission = dto.Permission != entity.Permission;

            entity.Title = dto.Title;
            entity.Name = dto.Name;
            entity.Icon = dto.Icon;
            entity.Path = dto.Path;
            entity.FunctionType = (FunctionType)dto.FunctionType;
            entity.Permission = dto.Permission;
            entity.ParentId = dto.ParentId;
            entity.Sort = dto.Sort;
            entity.Hidden = dto.Hidden;
            entity.Component = dto.Component;
            await _menuRepository.UpdateAsync(entity, true);

            if (updatePermission)
            {
                await identityDomainService.DelUserPermissionCacheByMenuIdAsync(entity.Id);
            }

            return true;
        }

        public async Task<List<FrontendRoute>> GenPowerRoutes()
        {
            var all = (await _menuRepository.GetQueryableAsync()).Where(x => x.FunctionType == FunctionType.Menu).ToList();
            var top = all.Where(x => !x.ParentId.HasValue || x.ParentId == Guid.Empty).OrderBy(x => x.Sort).ToList();
            var topMap = new List<FrontendRoute>();
            foreach (var item in top)
            {
                topMap.Add(new FrontendRoute
                {
                    Path = item.Path,
                    Component = item.Component,
                    Access = item.Permission,
                    Routes = getChildren(item.Id),
                });
            }

            List<FrontendRoute>? getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                if (children.Count == 0) return null;
                var childrenMap = new List<FrontendRoute>();
                foreach (var item in children)
                {
                    childrenMap.Add(new FrontendRoute
                    {
                        Path = item.Path,
                        Component = item.Component,
                        Access = item.Permission,
                        Routes = getChildren(item.Id)
                    });
                }
                return childrenMap;
            }

            return topMap;
        }
    }
}