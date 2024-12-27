using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

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
        private readonly IRepository<SysLoginLog> loginLogRepository;
        private readonly ICacheProvider cacheProvider;

        public AccountService(IRepository<SysUser> userRepository, ICurrentUser currentUser,
            IRepository<SysUserRole> userRoleRepository, IRepository<SysRoleMenu> roleMenuRepository, IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository, IConfiguration configuration, IReHeader reHeader, IRepository<SysLoginLog> loginLogRepository
            , ICacheProvider cacheProvider)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _configuration = configuration;
            _reHeader = reHeader;
            this.loginLogRepository = loginLogRepository;
            this.cacheProvider = cacheProvider;
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
            var userInfo = await GetUserInfoAsync(uid, expiredHour) ?? throw new BusinessException(message: "用户不存在");

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
            await cacheProvider.SetAsync(refreshToken, userInfo.UserName, TimeSpan.FromHours(expiredHour + 2));
            return rs;
        }

        public async Task<TokenResultDto> GetAccessTokenAsync(string refreshToken)
        {
            if (!await cacheProvider.ExistsAsync(refreshToken)) throw new BusinessException(message: "刷新token已过期");
            var username = await cacheProvider.GetAsync<string>(refreshToken);
            var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower()) ?? throw new BusinessException(message: "用户不存在");
            var rs = await GetTokenAsync(user.Id);
            await cacheProvider.DelAsync(refreshToken);
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
            if (await cacheProvider.ExistsAsync(key)) return await cacheProvider.GetAsync<UserInfoDto>(key);
            var user = (await _userRepository.FindAsync(x => x.Id == uid))!;
            var roleIds = (await _userRoleRepository.GetQueryableAsync()).Where(x => x.UserId == uid).Select(x => x.RoleId).ToList();
            var roles = await _roleRepository.GetListAsync(x => roleIds.Contains(x.Id));
            var isSuperAdmin = roles.Any(r => r.RoleName == AdminConsts.SuperAdminRole);
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
            await cacheProvider.SetAsync(key, rs, TimeSpan.FromHours(expiredHour));
            return rs;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto dto)
        {
            var loginLog = new SysLoginLog
            {
                IsSuccess = true,
                Ip = _reHeader.Ip,
                Os = _reHeader.Os,
                Address = _reHeader.Address,
                Browser = _reHeader.Browser,
                OperationMsg = "登录成功",
                UserName = dto.UserName
            };
            try
            {
                var user = await _userRepository.SingleOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.IsEnabled) ?? throw new BusinessException(message: "账号或密码不存在");
                var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.Password, user.PasswordSalt);
                if (!isRight) throw new BusinessException(message: "密码错误");
                var rs = (LoginResultDto)await GetTokenAsync(user.Id);
                return rs;
            }
            catch (Exception ex)
            {
                loginLog.IsSuccess = false;
                loginLog.OperationMsg = ex.Message;
                throw new BusinessException(message: ex.Message);
            }
            finally
            {
                await loginLogRepository.InsertAsync(loginLog, true);
            }
        }

        public async Task<List<FrontRoute>> GetFrontRoutes(int listStruct = 0)
        {
            var userInfo = await GetUserInfoAsync() ?? throw new BusinessException(message: "用户不存在");
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
                ?? throw new BusinessException(message: "用户不存在");
            user.Avatar = dto.Avatar;
            user.NickName = dto.NickName;
            user.Sex = dto.Sex;
            await _userRepository.UpdateAsync(user);
            await cacheProvider.DelAsync(UserCacheHelper.GetUserInfoKey(user.Id));
            return true;
        }

        public async Task<bool> UpdateUserPwdAsync(UserPwdDto dto)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Id == _currentUser.Id)
                ?? throw new BusinessException(message: "用户不存在");
            var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.OldPwd, user.PasswordSalt);
            if (!isRight) throw new BusinessException(message: "旧密码错误");
            if (dto.NewPwd != dto.SurePwd)
            {
                throw new BusinessException(message: "两次密码不一致");
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
            await cacheProvider.DelAsync(key);
            //移除刷新token
            var refreshToken = _reHeader.HttpContext.User.FindFirst(ClaimTypes.UserData)!.Value;
            await cacheProvider.DelAsync(refreshToken);
            return true;
        }
    }
}