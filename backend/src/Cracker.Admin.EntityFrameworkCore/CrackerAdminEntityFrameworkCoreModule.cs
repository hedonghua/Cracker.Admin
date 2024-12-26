using System.Data;

using Cracker.Admin.DAO;
using Cracker.Admin.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Cracker.Admin.EntitiesFrameworkCore;

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

        context.Services.AddTransient<IDbConnection>(sp =>
        {
            var context = sp.GetRequiredService<CrackerAdminDbContext>();
            return context.Database.GetDbConnection();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });

        context.Services.AddTransient<IUserDapperRepository, UserDapperRepository>();
        context.Services.AddTransient<IRoleDapperRepository, RoleDapperRepository>();
    }
}