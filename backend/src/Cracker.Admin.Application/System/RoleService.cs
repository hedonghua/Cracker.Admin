using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Cracker.Admin.System.Dtos;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace Cracker.Admin.System
{
    public class RoleService : ApplicationService, IRoleService
    {
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IdentityDomainService identityDomainService;

        public RoleService(IRepository<SysRole> roleRepository, IRepository<SysRoleMenu> roleMenuRepository
            , IRepository<SysUserRole> userRoleRepository, IdentityDomainService identityDomainService)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _userRoleRepository = userRoleRepository;
            this.identityDomainService = identityDomainService;
        }

        public async Task<bool> AddRoleAsync(RoleDto dto)
        {
            var isExist = await _roleRepository.AnyAsync(x => x.RoleName.ToLower() == dto.RoleName.ToLower());
            if (isExist)
            {
                throw new AbpValidationException("角色名已存在");
            }
            var entity = new SysRole
            {
                RoleName = dto.RoleName,
                Remark = dto.Remark
            };
            await _roleRepository.InsertAsync(entity, true);
            return true;
        }

        [UnitOfWork(true)]
        public async Task<bool> AssignMenuAsync(AssignMenuDto dto)
        {
            await _roleMenuRepository.DeleteDirectAsync(x => x.RoleId == dto.RoleId);
            if (dto.MenuIds != null)
            {
                var items = new List<SysRoleMenu>();
                foreach (var item in dto.MenuIds)
                {
                    items.Add(new SysRoleMenu
                    {
                        RoleId = dto.RoleId,
                        MenuId = item
                    });
                }
                if (items.Count > 0)
                {
                    await _roleMenuRepository.InsertManyAsync(items, true);
                }
            }
            await identityDomainService.DelUserPermissionCacheByRoleIdAsync(dto.RoleId);
            return true;
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            var hasUsers = await _userRoleRepository.AnyAsync(x => x.RoleId == id);
            if (hasUsers) throw new BusinessException(message: "角色已分配给用户，不能删除");
            await _roleRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<PagedResultDto<RoleListDto>> GetRoleListAsync(RoleQueryDto dto)
        {
            var query = (await _roleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.RoleName), x => x.RoleName.Contains(dto.RoleName!));
            var count = query.Count();
            var rows = query.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return new PagedResultDto<RoleListDto>(count, ObjectMapper.Map<List<SysRole>, List<RoleListDto>>(rows));
        }

        public async Task<List<AppOption>> GetRoleOptionsAsync()
        {
            return (await _roleRepository.GetQueryableAsync()).Select(x => new AppOption
            {
                Label = x.RoleName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public async Task<bool> UpdateRoleAsync(RoleDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));
            var entity = await _roleRepository.FindAsync(x => x.Id == dto.Id)
                ?? throw new AbpValidationException("数据不存在");
            var isExist = await _roleRepository.AnyAsync(x => x.RoleName.ToLower() == dto.RoleName.ToLower());
            if (entity.RoleName.ToLower() != dto.RoleName.ToLower() && isExist)
            {
                throw new AbpValidationException("角色名已存在");
            }
            entity.RoleName = dto.RoleName;
            entity.Remark = dto.Remark;
            await _roleRepository.UpdateAsync(entity, true);
            return true;
        }

        public async Task<Guid[]> GetRoleMenuIdsAsync(Guid id)
        {
            return [.. (await _roleMenuRepository.GetQueryableAsync()).Where(x => x.RoleId == id).Select(x => x.MenuId)];
        }
    }
}