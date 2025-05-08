using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Enums;
using Cracker.Admin.Extensions;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Users;

namespace Cracker.Admin.Account
{
    public class AccountService : ApplicationService, IAccountService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IConfiguration _configuration;
        private readonly IReHeader _reHeader;
        private readonly IDatabase redisDb;
        private readonly IdentityDomainService identityDomainService;
        private readonly ILocalEventBus localEventBus;
        private const int expired = 4;

        public AccountService(IRepository<SysUser> userRepository, ICurrentUser currentUser, IRepository<SysMenu> menuRepository
            , IConfiguration configuration, IReHeader reHeader, IDatabase redisDb, IdentityDomainService identityDomainService
            , ILocalEventBus localEventBus)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _menuRepository = menuRepository;
            _configuration = configuration;
            _reHeader = reHeader;
            this.redisDb = redisDb;
            this.identityDomainService = identityDomainService;
            this.localEventBus = localEventBus;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<(TokenResultDto tokenRes, string sessionId)> GenerateTokenAsync(Guid userId, string userName)
        {
            var time = DateTime.Now;

            var refreshToken = Guid.NewGuid().ToString("N").ToLower();
            var sessionId = SnowflakeHelper.Instance.NextId().ToString();
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Name, userName),
                new(AdminConsts.SessionId, sessionId)
            };
            var tokenExpired = time.AddHours(expired);
            var rs = new LoginResultDto
            {
                UserName = userName,
                ExpiredTime = tokenExpired,
                AccessToken = identityDomainService.GenerateToken(claims, tokenExpired),
                RefreshToken = refreshToken
            };

            if (!bool.Parse(_configuration["App:AccountManyLogin"]!))
            {
                //移除其它记录token
                await redisDb.KeyDeleteByPatternAsync("AccessToken:" + userId + ":*");
                await redisDb.KeyDeleteByPatternAsync("RefreshToken:" + userId + ":*");
            }

            await redisDb.StringSetAsync($"AccessToken:{userId}:{sessionId}", rs.AccessToken, TimeSpan.FromHours(expired));
            await redisDb.StringSetAsync($"RefreshToken:{userId}:{sessionId}", refreshToken, TimeSpan.FromHours(expired + 1));

            return (rs, sessionId);
        }

        public async Task<TokenResultDto> GetAccessTokenAsync(string refreshToken)
        {
            if (!_currentUser.Id.HasValue) throw new AbpAuthorizationException();

            var sessionId = _currentUser.FindClaim(AdminConsts.SessionId)!.Value;
            var key = $"RefreshToken:{_currentUser.Id.Value}:{sessionId}";
            if (!await redisDb.KeyExistsAsync(key)) throw new BusinessException(message: "刷新token已过期");

            var existRefreshToken = await redisDb.StringGetAsync(key);
            if (!refreshToken.Equals(existRefreshToken)) throw new BusinessException(message: "刷新token不正确");

            return (await GenerateTokenAsync(_currentUser.Id.Value, _currentUser.UserName!)).tokenRes;
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
                var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower() && x.IsEnabled) ?? throw new BusinessException(message: "账号或密码不存在");
                var isRight = user.Password == EncryptionHelper.GenEncodingPassword(dto.Password, user.PasswordSalt);
                if (!isRight) throw new BusinessException(message: "密码错误");

                var (tokenRes, sessionId) = await GenerateTokenAsync(user.Id, user.UserName);

                loginLog.SessionId = sessionId;

                var rs = ObjectMapper.Map<TokenResultDto, LoginResultDto>(tokenRes);
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
                await localEventBus.PublishAsync(loginLog);
            }
        }

        public async Task<List<FrontendMenu>> GetFrontMenus(int listStruct = 0)
        {
            var permission = await identityDomainService.GetUserPermissionAsync(_currentUser.Id!.Value);
            if (permission.MenuIds == null || permission.MenuIds.Length <= 0) return [];

            var all = (await _menuRepository.GetQueryableAsync())
                .Where(x => permission.MenuIds.Contains(x.Id) && x.FunctionType == FunctionType.Menu).ToList();
            if (listStruct == 1) return all.Where(x => x.ParentId != Guid.Empty && x.ParentId.HasValue).OrderBy(x => x.ParentId).ThenBy(x => x.Sort).Select(x => new FrontendMenu
            {
                Id = x.Id,
                Path = x.Path,
                Component = x.Component,
                Name = x.Title,
                Key = x.Name,
                Icon = x.Icon,
                HideInMenu = x.Hidden,
            }).ToList();
            var top = all.Where(x => !x.ParentId.HasValue || x.ParentId == Guid.Empty).OrderBy(x => x.Sort).ToList();
            var topMap = new List<FrontendMenu>();
            foreach (var item in top)
            {
                topMap.Add(new FrontendMenu
                {
                    Id = item.Id,
                    Path = item.Path,
                    Component = item.Component,
                    Name = item.Title,
                    Key = item.Name,
                    Icon = item.Icon,
                    HideInMenu = item.Hidden,
                    Children = getChildren(item.Id),
                });
            }

            List<FrontendMenu> getChildren(Guid currentId)
            {
                var children = all.Where(x => x.ParentId == currentId).OrderBy(x => x.Sort).ToList();
                var childrenMap = new List<FrontendMenu>();
                foreach (var item in children)
                {
                    childrenMap.Add(new FrontendMenu
                    {
                        Id = item.Id,
                        Path = item.Path,
                        Component = item.Component,
                        Key = item.Name,
                        Name = item.Title,
                        Icon = item.Icon,
                        HideInMenu = item.Hidden,
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
            if (user.NickName.ToLower() != dto.NickName!.ToLower())
            {
                var exist = await _userRepository.AnyAsync(x => x.NickName.ToLower() == dto.NickName.ToLower());
                if (exist) throw new BusinessException(message: "昵称已占用");
            }
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
            user.PasswordSalt = EncryptionHelper.GetPasswordSalt();
            user.Password = EncryptionHelper.GenEncodingPassword(dto.NewPwd, user.PasswordSalt);
            await _userRepository.UpdateAsync(user, true);
            return true;
        }

        public async Task<bool> SignOutAsync()
        {
            var uid = _currentUser.Id!.Value;
            var sessionId = _currentUser.FindClaim(AdminConsts.SessionId)!.Value;
            //移除访问token
            await redisDb.KeyDeleteAsync($"AccessToken:{uid}:{sessionId}");
            //移除刷新token
            await redisDb.KeyDeleteAsync($"RefreshToken:{uid}:{sessionId}");
            return true;
        }
    }
}