using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cracker.Admin.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */

public class CrackerAdminDbContextFactory : IDesignTimeDbContextFactory<CrackerAdminDbContext>
{
    public CrackerAdminDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CrackerAdminDbContext>()
            .UseMySql(configuration.GetConnectionString("Default"), ServerVersion.Create(Version.Parse("8.1.0"), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));

        return new CrackerAdminDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Cracker.Admin.HttpApi.Host/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}