using System.Data;

namespace Cracker.Admin.Core
{
    public interface IDapperFactory
    {
        IDbConnection CreateInstance(string connectionString);
    }
}