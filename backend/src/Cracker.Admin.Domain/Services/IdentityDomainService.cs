using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace Cracker.Admin.Services
{
    public class IdentityDomainService : DomainService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IDatabase redisDb;
        private readonly IConfiguration configuration;

        public IdentityDomainService(IRepository<SysUser> userRepository, ICurrentUser currentUser,
            IRepository<SysUserRole> userRoleRepository, IRepository<SysRoleMenu> roleMenuRepository, IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository, IDatabase redisDb, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            this.redisDb = redisDb;
            this.configuration = configuration;
        }

        public async Task<UserPermission> GetUserPermissionAsync(Guid userId)
        {
            var key = "UserPermission:" + userId;
            if (await redisDb.KeyExistsAsync(key)) return (await redisDb.GetObjectAsync<UserPermission>(key))!;

            var roleIds = (await _userRoleRepository.GetQueryableAsync()).Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            var roles = await _roleRepository.GetListAsync(x => roleIds.Contains(x.Id));
            var isSuperAdmin = roles.Any(r => r.RoleName == AdminConsts.SuperAdminRole);
            var menuIds = (await _roleMenuRepository.GetQueryableAsync()).Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).ToList();
            var menus = (await _menuRepository.GetQueryableAsync()).Where(x => menuIds.Contains(x.Id) || isSuperAdmin).Select(x => new { x.Permission, x.Id }).ToList();
            if (isSuperAdmin)
            {
                menuIds = menus.Select(x => x.Id).ToList();
            }
            var rs = new UserPermission
            {
                UserId = userId,
                Roles = roles.Select(c => c.RoleName).ToArray(),
                Auths = menus.Where(c => !string.IsNullOrEmpty(c.Permission)).Select(c => c.Permission!).ToArray(),
                RoleIds = [.. roleIds],
                MenuIds = [.. menuIds]
            };
            await redisDb.SetObjectAsync(key, rs);
            return rs;
        }

        public async Task DelUserPermissionCacheByRoleIdAsync(Guid roleId)
        {
            var userRoles = await _userRoleRepository.GetListAsync(x => x.RoleId == roleId);
            foreach (var item in userRoles)
            {
                await redisDb.KeyDeleteAsync("UserPermission:" + item.UserId);
            }
        }

        public async Task DelUserPermissionCacheByUserIdAsync(Guid userId)
        {
            await redisDb.KeyDeleteAsync("UserPermission:" + userId);
        }

        public async Task DelAdminUserPermissionCacheAsync()
        {
            var adminRole = await _roleRepository.FirstOrDefaultAsync(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole);
            if (adminRole == null) return;
            await this.DelUserPermissionCacheByRoleIdAsync(adminRole.Id);
        }

        public async Task DelUserPermissionCacheByMenuIdAsync(Guid menuId)
        {
            var roleIds = (await _roleMenuRepository.GetQueryableAsync()).Where(x => x.MenuId == menuId).Select(x => x.RoleId).ToList();
            foreach (var item in roleIds)
            {
                await redisDb.KeyDeleteAsync("UserPermission:" + item);
            }
        }

        public async Task<bool> CheckTokenAsync(string userId, string sessionId, string token)
        {
            var existToken = await redisDb.StringGetAsync($"AccessToken:{userId}:{sessionId}");
            return token == existToken;
        }

        public async Task<bool> CheckPermissionAsync(string userId, string code)
        {
            var permission = await GetUserPermissionAsync(Guid.Parse(userId));
            if (permission == null || permission.Auths == null) return false;

            return permission.Auths.Contains(code);
        }

        public string GenerateToken(IEnumerable<Claim> claims, DateTime expireTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["IssuerSigningKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: configuration.GetSection("Jwt")["ValidIssuer"],
                audience: configuration.GetSection("Jwt")["ValidAudience"],
                claims: claims,
                expires: expireTime,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        /// <summary>
        /// 从Token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal? GetPrincipalFromAccessToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["IssuerSigningKey"]!));
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}