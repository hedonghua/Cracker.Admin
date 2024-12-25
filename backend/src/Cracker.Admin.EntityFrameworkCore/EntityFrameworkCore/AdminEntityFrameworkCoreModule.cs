using System.Data;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Cracker.Admin.EntityFrameworkCore;

[DependsOn(
    typeof(AbpDapperModule),
    typeof(AdminDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class AdminEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdminEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AdminDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        context.Services.AddTransient<IDbConnection>(sp =>
        {
            var context = sp.GetRequiredService<AdminDbContext>();
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