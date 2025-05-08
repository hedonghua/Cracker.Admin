using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IDatabase redisDb;
        private readonly IConfiguration configuration;
        private readonly IDapperFactory dapperFactory;
        private readonly ICurrentUser currentUser;
        private readonly IRepository<SysRoleDept> roleDeptRepository;
        private readonly IRepository<OrgDept> deptRepository;
        private readonly IRepository<OrgEmployee> employeeRepository;

        public IdentityDomainService(IRepository<SysUserRole> userRoleRepository, IRepository<SysRoleMenu> roleMenuRepository, IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository, IDatabase redisDb, IConfiguration configuration, IDapperFactory dapperFactory, ICurrentUser currentUser,
            IRepository<SysRoleDept> roleDeptRepository, IRepository<OrgDept> deptRepository, IRepository<OrgEmployee> employeeRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            this.redisDb = redisDb;
            this.configuration = configuration;
            this.dapperFactory = dapperFactory;
            this.currentUser = currentUser;
            this.roleDeptRepository = roleDeptRepository;
            this.deptRepository = deptRepository;
            this.employeeRepository = employeeRepository;
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                Auths = menus.Where(c => !string.IsNullOrEmpty(c.Permission)).Select(c => c.Permission!).Distinct().ToArray(),
                RoleIds = [.. roleIds],
                MenuIds = [.. menuIds],
                IsSuperAdmin = isSuperAdmin
            };
            await redisDb.SetObjectAsync(key, rs);
            return rs;
        }

        /// <summary>
        /// 删除用户权限缓存（通过角色ID）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task DelUserPermissionCacheByRoleIdAsync(Guid roleId)
        {
            var userRoles = await _userRoleRepository.GetListAsync(x => x.RoleId == roleId);
            foreach (var item in userRoles)
            {
                await redisDb.KeyDeleteAsync("UserPermission:" + item.UserId);
            }
        }

        /// <summary>
        /// 删除用户权限缓存（通过用户ID）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DelUserPermissionCacheByUserIdAsync(Guid userId)
        {
            await redisDb.KeyDeleteAsync("UserPermission:" + userId);
        }

        /// <summary>
        /// 删除超级管理员用户权限缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DelAdminUserPermissionCacheAsync()
        {
            var adminRoleId = (await _roleRepository.GetQueryableAsync()).Where(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole).Select(x => x.Id).FirstOrDefault();
            if (adminRoleId == Guid.Empty) return;
            await this.DelUserPermissionCacheByRoleIdAsync(adminRoleId);
        }

        /// <summary>
        /// 删除用户权限缓存（通过菜单ID，通过RoleMenu关系删除）
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task DelUserPermissionCacheByMenuIdAsync(Guid menuId)
        {
            var roleIds = (await _roleMenuRepository.GetQueryableAsync()).Where(x => x.MenuId == menuId).Select(x => x.RoleId).ToList();
            var adminRoleId = (await _roleRepository.GetQueryableAsync()).Where(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole).Select(x => x.Id).FirstOrDefault();
            if (adminRoleId != Guid.Empty)
            {
                roleIds.Add(adminRoleId);
            }
            foreach (var item in roleIds)
            {
                await this.DelUserPermissionCacheByRoleIdAsync(item);
            }
        }

        /// <summary>
        /// 检查Token是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CheckTokenAsync(string userId, string sessionId, string token)
        {
            var existToken = await redisDb.StringGetAsync($"AccessToken:{userId}:{sessionId}");
            return token == existToken;
        }

        /// <summary>
        /// 检查用户是否有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> CheckPermissionAsync(string userId, string code)
        {
            var permission = await GetUserPermissionAsync(Guid.Parse(userId));
            if (permission == null || permission.Auths == null) return false;

            return permission.Auths.Contains(code) || permission.IsSuperAdmin;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 用户是否来源主库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UserIsFromMainDbAsync(string id)
        {
            var defaultConnectionString = configuration.GetConnectionString("Default");
            var connection = dapperFactory.CreateInstance(defaultConnectionString!);
            return await connection.ExecuteScalarAsync<int>("select count(*) from sys_user where id = @id", new { id = id }) > 0;
        }

        /// <summary>
        /// 获取当前用户可查看的相关部门，员工
        /// </summary>
        /// <returns></returns>
        public async Task<PowerDataId> GetPowerData()
        {
            var data = new PowerDataId() { DeptIds = [], EmployeeIds = [] };
            var permission = await GetUserPermissionAsync(currentUser.Id!.Value);
            if (permission.RoleIds == null || permission.RoleIds.Length == 0) return data;

            var roles = await _roleRepository.GetListAsync(x => permission.RoleIds != null && permission.RoleIds.Contains(x.Id));
            var employee = await employeeRepository.FirstOrDefaultAsync(x => x.UserId == currentUser.Id);
            foreach (var role in roles)
            {
                if (role.PowerDataType == 0)
                {
                    data.DeptIds.AddRange([.. (await deptRepository.GetQueryableAsync()).Select(x => x.Id)]);
                }
                else if (role.PowerDataType == 1)
                {
                    data.DeptIds.AddRange([.. (await roleDeptRepository.GetQueryableAsync()).Where(x => x.RoleId == role.Id).Select(x => x.DeptId)]);
                }
                else if (role.PowerDataType == 2)
                {
                    if (employee != null)
                    {
                        data.DeptIds.AddRange([.. (await deptRepository.GetQueryableAsync()).Where(x => x.Id == employee.DeptId || (x.ParentIds != null && x.ParentIds!.Contains(employee.DeptId.ToString()))).Select(x => x.Id)]);
                    }
                }
                else if (role.PowerDataType == 3)
                {
                    if (employee != null)
                    {
                        data.DeptIds.Add(employee.DeptId);
                    }
                }
                else if (role.PowerDataType == 4)
                {
                    if (employee != null)
                    {
                        data.DeptIds.Add(employee.DeptId);
                        data.EmployeeIds.Add(employee.Id);
                    }
                }

                if (role.PowerDataType != 4)
                {
                    data.EmployeeIds.AddRange([.. (await employeeRepository.GetQueryableAsync()).Where(x => data.DeptIds != null && data.DeptIds.Contains(x.DeptId)).Select(x => x.Id)]);
                }
            }

            return data;
        }
    }
}