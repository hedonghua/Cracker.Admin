using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using Cracker.Admin.Services;
using Cracker.Admin.System.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace Cracker.Admin.System
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IdentityDomainService identityDomainService;
        private readonly IConfiguration configuration;

        public UserService(IRepository<SysUser> userRepository, IRepository<SysUserRole> userRoleRepository
            , IdentityDomainService identityDomainService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            this.identityDomainService = identityDomainService;
            this.configuration = configuration;
        }

        public async Task<bool> AddUserAsync(UserDto dto)
        {
            var isExist = await _userRepository.AnyAsync(x => x.UserName.ToLower() == dto.UserName.ToLower());
            if (isExist)
            {
                throw new AbpValidationException("账号已存在");
            }
            var user = new SysUser
            {
                UserName = dto.UserName,
                PasswordSalt = EncryptionHelper.GetPasswordSalt(),
                Avatar = dto.Avatar,
                NickName = dto.NickName ?? dto.UserName,
                Sex = dto.Sex,
                IsEnabled = dto.IsEnabled
            };
            if (string.IsNullOrWhiteSpace(dto.Avatar))
            {
                user.Avatar = configuration["Oss:Domain"] + (user.Sex == 1 ? AdminConsts.AvatarMale : AdminConsts.AvatarFemale);
            }
            user.Password = EncryptionHelper.GenEncodingPassword(dto.Password, user.PasswordSalt);
            await _userRepository.InsertAsync(user, true);
            return true;
        }

        [UnitOfWork(true)]
        public async Task<bool> AssignRoleAsync(AssignRoleDto dto)
        {
            await _userRoleRepository.DeleteDirectAsync(x => x.UserId == dto.UserId);
            if (dto.RoleIds != null)
            {
                var items = new List<SysUserRole>();
                foreach (var item in dto.RoleIds)
                {
                    items.Add(new SysUserRole
                    {
                        UserId = dto.UserId,
                        RoleId = item
                    });
                }
                if (items.Count > 0)
                {
                    await _userRoleRepository.InsertManyAsync(items, true);
                }
            }
            await identityDomainService.DelUserPermissionCacheByUserIdAsync(dto.UserId);
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(x => x.Id == id);
            return true;
        }

        public async Task<PagedResultDto<UserListDto>> GetUserListAsync(UserQueryDto dto)
        {
            var query = (await _userRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.UserName), x => x.UserName.Contains(dto.UserName!));
            var count = query.Count();
            var rows = query.Skip((dto.Current - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            return new PagedResultDto<UserListDto>(count, ObjectMapper.Map<List<SysUser>, List<UserListDto>>(rows));
        }

        public async Task<Guid[]> GetUserRoleIdsAsync(Guid uid)
        {
            return [.. (await _userRoleRepository.GetQueryableAsync()).Where(x => x.UserId == uid).Select(x => x.RoleId)];
        }

        public async Task<bool> SwitchUserEnabledStatusAsync(Guid id)
        {
            var entity = await _userRepository.FindAsync(x => x.Id == id)
                ?? throw new AbpValidationException("数据不存在");
            entity.IsEnabled = !entity.IsEnabled;
            await _userRepository.UpdateAsync(entity, true);
            return true;
        }

        public async Task ResetUserPasswordAsync(ResetUserPwdDto dto)
        {
            var user = await _userRepository.GetAsync(x => x.Id == dto.UserId);
            user.PasswordSalt = EncryptionHelper.GetPasswordSalt();
            user.Password = EncryptionHelper.GenEncodingPassword(dto.Password!, user.PasswordSalt);
            await _userRepository.UpdateAsync(user, true);
        }
    }
}