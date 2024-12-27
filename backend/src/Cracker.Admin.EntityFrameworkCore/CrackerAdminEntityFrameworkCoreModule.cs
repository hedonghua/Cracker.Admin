using Cracker.Admin.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Cracker.Admin;

[DependsOn(
    typeof(AbpDapperModule),
    typeof(CrackerAdminDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class CrackerAdminEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CrackerAdminDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });

        context.Services.AddTransient<IUserDapperRepository, UserDapperRepository>();
        context.Services.AddTransient<IRoleDapperRepository, RoleDapperRepository>();
        context.Services.AddTransient<IDatabaseDapperRepository, DatabaseDapperRepository>();
    }
}