using System.Data;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Cracker.Admin.EntityFrameworkCore;

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
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        context.Services.AddTransient<IDbConnection>(sp =>
        {
            var context = sp.GetRequiredService<CrackerAdminDbContext>();
            return context.Database.GetDbConnection();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also AdminMigrationsDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });
    }
}