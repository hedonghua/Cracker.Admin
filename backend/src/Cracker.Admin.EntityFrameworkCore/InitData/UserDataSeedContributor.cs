using System.Threading.Tasks;

using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Encryption;

namespace Cracker.Admin.InitData
{
    public class UserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SysUser> userRepository;
        private readonly IStringEncryptionService stringEncryptionService;

        public UserDataSeedContributor(IRepository<SysUser> userRepository, IStringEncryptionService stringEncryptionService)
        {
            this.userRepository = userRepository;
            this.stringEncryptionService = stringEncryptionService;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var exist = await userRepository.AnyAsync(x => x.UserName.ToLower() == "admin");
            if (!exist)
            {
                var user = new SysUser
                {
                    UserName = "admin",
                    PasswordSalt = EncryptionHelper.GetPasswordSalt()
                };
                user.Password = EncryptionHelper.GenEncodingPassword("123qwe*", user.PasswordSalt);
                await userRepository.InsertAsync(user);
            }
        }
    }
}