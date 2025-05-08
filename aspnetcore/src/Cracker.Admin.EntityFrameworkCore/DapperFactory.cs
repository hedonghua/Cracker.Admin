using Cracker.Admin.Core;
using MySqlConnector;
using System.Data;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin
{
    public class DapperFactory : IDapperFactory, ISingletonDependency
    {
        public IDbConnection CreateInstance(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}