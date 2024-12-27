using Cracker.Admin.Entities;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.InitData
{
    public class RoleDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SysRole> roleRepository;

        public RoleDataSeedContributor(IRepository<SysRole> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var exist = await roleRepository.AnyAsync(x => x.RoleName.ToLower() == AdminConsts.SuperAdminRole);
            if (!exist)
            {
                var role = new SysRole
                {
                    RoleName = AdminConsts.SuperAdminRole,
                    Remark = "系统默认创建，拥有最高权限"
                };
                await roleRepository.InsertAsync(role, true);
            }
        }
    }
}