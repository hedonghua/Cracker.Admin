using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.InitData
{
    public class AdminUserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SysUser> userRepository;
        private readonly IRepository<SysRole> roleRepository;
        private readonly IRepository<SysUserRole> userRoleRepository;

        public AdminUserDataSeedContributor(IRepository<SysUser> userRepository, IRepository<SysRole> roleRepository, IRepository<SysUserRole> userRoleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var roleExist = await roleRepository.AnyAsync(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole);
            if (!roleExist)
            {
                var role = new SysRole
                {
                    RoleName = AdminConsts.SuperAdminRole,
                    Remark = "系统默认创建，拥有最高权限"
                };
                await roleRepository.InsertAsync(role, true);
            }

            var userExist = await userRepository.AnyAsync(x => x.UserName.ToLower() == AdminConsts.DefaultAdmin);
            if (!userExist)
            {
                var user = new SysUser
                {
                    UserName = AdminConsts.DefaultAdmin,
                    NickName = AdminConsts.DefaultAdmin,
                    PasswordSalt = EncryptionHelper.GetPasswordSalt(),
                    IsEnabled = true,
                    Avatar = AdminConsts.AvatarFemale
                };
                user.Password = EncryptionHelper.GenEncodingPassword(AdminConsts.DefaultAdminPassword, user.PasswordSalt);
                await userRepository.InsertAsync(user, true);
            }

            if (!roleExist || !userExist)
            {
                var userId = (await userRepository.GetQueryableAsync()).Where(x => x.UserName.ToLower() == AdminConsts.DefaultAdmin).Select(x => x.Id).First();
                var roleId = (await roleRepository.GetQueryableAsync()).Where(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole).Select(x => x.Id).First();
                var userRole = new SysUserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };
                await userRoleRepository.InsertAsync(userRole, true);
            }
        }
    }
}