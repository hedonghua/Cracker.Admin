using Microsoft.Extensions.Configuration;
using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.MyExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Cracker.Admin.Entities.Enums;
using Cracker.Admin.Entities;

namespace Cracker.Admin.Account
{
    public class AccountService : ApplicationService, IAccountService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IConfiguration _configuration;
        private readonly IReHeader _reHeader;

        public AccountService(IRepository<SysUser> userRepository, ICurrentUser currentUser,
            IRepository<SysUserRole> userRoleRepository, IRepository<SysRoleMenu> roleMenuRepository, IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository, IConfiguration configuration, IReHeader reHeader)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _configuration = configuration;
            _reHeader = reHeader;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<TokenResultDto> GetTokenAsync(Guid uid)
        {
            int expiredHour = 4;
            var time = DateTime.Now;
            var userInfo = await GetUserInfoAsync(uid, expiredHour) ?? throw new TipException("用户不存在");

            var refreshToken = Guid.NewGuid().ToString("N").ToLower();
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, userInfo.UserId.ToString()),
                new(ClaimTypes.Name, userInfo.UserName!),
                new(ClaimTypes.Role, string.Join(',',userInfo?.Roles ?? [])),
                new(ClaimTypes.UserData,refreshToken)
            };
            var rs = new LoginResultDto
            {
                UserName = userInfo!.UserName,
                ExpiredTime = time.AddHours(expiredHour).ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = TokenHelper.GenerateToken(claims, time.AddHours(expiredHour)),
                RefreshToken = refreshToken,
                Auths = userInfo.Auths
            };
            await RedisHelper.SetAsync(refreshToken, userInfo.UserName, TimeSpan.FromHours(expiredHour + 2));
            return rs;
        }

        public async Task<TokenResultDto> GetAccessTokenAsync(string refreshToken)
        {
            if (!RedisHelper.Exists(refreshToken)) throw new TipException("刷新token已过期");
            var username = RedisHelper.Get<string>(refreshToken);
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower()) ?? throw new TipException("用户不存在");
            var rs = await GetTokenAsync(user.Id);
            RedisHelper.Del(refreshToken);
            return rs;
        }

        public async Task<UserInfoDto> GetUserInfoAsync()
        {
            if (!_currentUser.Id.HasValue) throw new AbpAuthorizationException();
            return await GetUserInfoAsync(_currentUser.Id.Value);
        }

        private async Task<UserInfoDto> GetUserInfoAsync(Guid uid, int expiredHour = 6)
        {
            var key = UserCacheHelper.GetUserInfoKey(uid);
            if (RedisHelper.Exists(key)) return RedisHelper.Get<UserInfoDto>(key);
            var user = (await _userRepository.FindAsync(x => x.Id == uid))!;
            var roleIds = (await _userRoleRepository.GetQueryableAsync()).Where(x => x.UserId == uid).Select(x => x.RoleId).ToList();
            var roles = await _roleRepository.GetListAsync(x => roleIds.Contains(x.Id));
            var isSuperAdmin = roles.Any(r => r.RoleName == AdminConsts.SUPERADMIN);
            var menuIds = (await _roleMenuRepository.GetQueryableAsync()).Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).ToList();
            var menus = await _menuRepository.GetListAsync(x => menuIds.Contains(x.Id) || isSuperAdmin);
            if (isSuperAdmin)
            {
                menuIds = menus.Select(x => x.Id).ToList();
            }
            var rs = new UserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Sex = user.Sex,
                Roles = roles.Select(c => c.RoleName).ToArray(),
                Auths = menus.Where(c => !string.IsNullOrEmpty(c.Permission)).Select(c => c.Permission!).ToArray(),
                RoleIds = [.. roleIds],
                MenuIds = [.. menuIds]
            };
            await RedisHelper.SetAsync(key, rs, TimeSpan.FromHours(expiredHour));
            return rs;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto dto)
        {
            try
            {
                var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.IsEnabled) ?? throw new TipException("账号或密码不存在");
                var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.Password, user.PasswordSalt);
                if (!isRight) throw new TipException("密码错误");
                var rs = (LoginResultDto)await GetTokenAsync(user.Id);
                return rs;
            }
            catch (Exception ex)
            {
                throw new TipException(ex.Message);
            }
        }

        public async Task<List<FrontRoute>> GetFrontRoutes(int listStruct = 0)
        {
            var userInfo = await GetUserInfoAsync() ?? throw new TipException("用户不存在");
            if (userInfo.MenuIds == null) return [];

            var all = (await _menuRepository.GetQueryableAsync())
                .Where(x => userInfo.MenuIds.Contains(x.Id) && x.FunctionType == FunctionType.Menu).ToList();
            if (listStruct == 1) return all.Where(x => x.ParentId != Guid.Empty && x.ParentId.HasValue).OrderBy(x => x.ParentId).ThenBy(x => x.Sort).Select(x => new FrontRoute
            {
                Id = x.Id,
                Path = x.Path,
                Meta = new FrontRouteMeta
                {
                    Title = x.Title,
                    Icon = x.Icon,
                    Hidden = x.Hidden
                },
            }).ToList();
            var top = all.Where(x => !x.ParentId.HasValue || x.ParentId == Guid.Empty).OrderBy(x => x.Sort).ToList();
            var topMap = new List<FrontRoute>();
            foreach (var item in top)
            {
                topMap.Add(new FrontRoute
                {
                    Id = item.Id,
                    Path = item.Path,
                    Meta = new FrontRouteMeta
                    {
                        Title = item.Title,
                        Icon = item.Icon,
                        Hidden = item.Hidden
                    },
                    Children = getChildren(item.Id)
                });
            }

            List<FrontRoute> getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                var childrenMap = new List<FrontRoute>();
                foreach (var item in children)
                {
                    childrenMap.Add(new FrontRoute
                    {
                        Id = item.Id,
                        Path = item.Path,
                        Meta = new FrontRouteMeta
                        {
                            Title = item.Title,
                            Icon = item.Icon,
                            Hidden = item.Hidden
                        },
                        Children = getChildren(item.Id)
                    });
                }
                return childrenMap;
            }

            return topMap;
        }

        public async Task<bool> UpdateUserInfoAsync(PersonalInfoDto dto)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Id == _currentUser.Id)
                ?? throw new TipException("用户不存在");
            user.Avatar = dto.Avatar;
            user.NickName = dto.NickName;
            user.Sex = dto.Sex;
            await _userRepository.UpdateAsync(user);
            RedisHelper.Del(UserCacheHelper.GetUserInfoKey(user.Id));
            return true;
        }

        public async Task<bool> UpdateUserPwdAsync(UserPwdDto dto)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Id == _currentUser.Id)
                ?? throw new TipException("用户不存在");
            var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.OldPwd, user.PasswordSalt);
            if (!isRight) throw new TipException("旧密码错误");
            if (dto.NewPwd != dto.SurePwd)
            {
                throw new TipException("两次密码不一致");
            }
            user.PasswordSalt = EncryptionHelper.GetPasswordSalt();
            user.Password = EncryptionHelper.GenEncodingPassword(dto.NewPwd, user.PasswordSalt);
            await _userRepository.UpdateAsync(user, true);
            return true;
        }

        public async Task<bool> SignOutAsync()
        {
            var key = UserCacheHelper.GetUserInfoKey(_currentUser.Id.GetValueOrDefault());
            //移除用户信息
            await RedisHelper.DelAsync(key);
            //移除刷新token
            var refreshToken = _reHeader.HttpContext.User.FindFirst(ClaimTypes.UserData)!.Value;
            await RedisHelper.DelAsync(refreshToken);
            return true;
        }
    }
}