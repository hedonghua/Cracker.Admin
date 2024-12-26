using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;

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
            var exist = await userRepository.AnyAsync(x => x.UserName.ToLower() == "admin");
            if (!exist)
            {
                var user = new SysUser
                {
                    UserName = "admin",
                    NickName = "admin",
                    PasswordSalt = EncryptionHelper.GetPasswordSalt(),
                    IsEnabled = true,
                };
                user.Password = EncryptionHelper.GenEncodingPassword("123qwe*", user.PasswordSalt);
                await userRepository.InsertAsync(user);
            }
        }
    }
}