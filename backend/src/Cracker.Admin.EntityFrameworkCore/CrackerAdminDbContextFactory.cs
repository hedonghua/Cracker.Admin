using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Cracker.Admin;

public class CrackerAdminDbContextFactory : IDesignTimeDbContextFactory<CrackerAdminDbContext>
{
    public CrackerAdminDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString("Default");

        return CreateDbContext(connectionString!);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Cracker.Admin.HttpApi.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }

    public static CrackerAdminDbContext CreateDbContext(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<CrackerAdminDbContext>()
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new CrackerAdminDbContext(builder.Options);
    }
}