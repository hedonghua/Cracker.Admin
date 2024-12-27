using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Mapster;
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
        private readonly IdentityDomainService identityDomainService;
        private const int expired = 4;

        public AccountService(IRepository<SysUser> userRepository, ICurrentUser currentUser,
            IRepository<SysUserRole> userRoleRepository, IRepository<SysRoleMenu> roleMenuRepository, IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository, IConfiguration configuration, IReHeader reHeader, IRepository<SysLoginLog> loginLogRepository
            , ICacheProvider cacheProvider, IdentityDomainService identityDomainService)
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
            this.identityDomainService = identityDomainService;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<TokenResultDto> GenerateTokenAsync(Guid userId, string userName)
        {
            var time = DateTime.Now;

            var refreshToken = Guid.NewGuid().ToString("N").ToLower();
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Name, userName)
            };
            var tokenExpired = time.AddHours(expired);
            var rs = new LoginResultDto
            {
                UserName = userName,
                ExpiredTime = tokenExpired.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = TokenHelper.GenerateToken(claims, tokenExpired),
                RefreshToken = refreshToken
            };
            await cacheProvider.SetAsync("AccessToken:" + userId, rs.AccessToken, TimeSpan.FromHours(expired));
            await cacheProvider.SetAsync("RefreshToken:" + userId, refreshToken, TimeSpan.FromHours(expired + 1));

            return rs;
        }

        public async Task<TokenResultDto> GetAccessTokenAsync(string refreshToken)
        {
            if (!_currentUser.Id.HasValue) throw new AbpAuthorizationException();

            var key = "RefreshToken:" + _currentUser.Id.Value;
            if (!await cacheProvider.ExistsAsync(key)) throw new BusinessException(message: "刷新token已过期");

            var existRefreshToken = await cacheProvider.GetAsync<string>(key);
            if (!refreshToken.Equals(existRefreshToken)) throw new BusinessException(message: "刷新token不正确");

            return await GenerateTokenAsync(_currentUser.Id.Value, _currentUser.UserName!);
        }

        public async Task<UserInfoDto> GetUserInfoAsync()
        {
            if (!_currentUser.Id.HasValue) throw new AbpAuthorizationException();

            var permission = await identityDomainService.GetUserPermissionAsync(_currentUser.Id.Value);
            var user = await _userRepository.GetAsync(x => x.Id == _currentUser.Id.Value);
            return new UserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                Sex = user.Sex,
                Roles = permission.Roles,
                Auths = permission.Auths,
                RoleIds = [.. permission.RoleIds],
                MenuIds = [.. permission.MenuIds]
            };
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
                var user = await _userRepository.GetAsync(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.IsEnabled) ?? throw new BusinessException(message: "账号或密码不存在");
                var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.Password, user.PasswordSalt);
                if (!isRight) throw new BusinessException(message: "密码错误");

                var rs = (await GenerateTokenAsync(user.Id, user.UserName)).Adapt<LoginResultDto>();
                var permission = await identityDomainService.GetUserPermissionAsync(user.Id);
                rs.UserName = dto.UserName;
                rs.Auths = permission.Auths;

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
            var permission = await identityDomainService.GetUserPermissionAsync(_currentUser.Id!.Value);
            if (permission.MenuIds == null || permission.MenuIds.Length <= 0) return [];

            var all = (await _menuRepository.GetQueryableAsync())
                .Where(x => permission.MenuIds.Contains(x.Id) && x.FunctionType == FunctionType.Menu).ToList();
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
            var user = await _userRepository.GetAsync(x => x.Id == _currentUser.Id);
            user.Avatar = dto.Avatar;
            user.NickName = dto.NickName;
            user.Sex = dto.Sex;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserPwdAsync(UserPwdDto dto)
        {
            var user = await _userRepository.GetAsync(x => x.Id == _currentUser.Id)
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
            var uid = _currentUser.Id!.Value;
            //移除访问token
            await cacheProvider.DelAsync("AccessToken:" + uid);
            //移除刷新token
            await cacheProvider.DelAsync("RefreshToken:" + uid);
            return true;
        }
    }
}