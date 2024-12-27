using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.InitData
{
    public class UserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SysUser> userRepository;

        public UserDataSeedContributor(IRepository<SysUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var exist = await userRepository.AnyAsync(x => x.UserName.ToLower() == AdminConsts.DefaultAdmin);
            if (!exist)
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
        }
    }
}