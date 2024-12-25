using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Database
{
    public interface IAdminDbContextFactory
    {
        DbContext CreateDbContext();
    }
}