using Dapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin.Repositories
{
    public class UserDapperRepository : DapperRepository<CrackerAdminDbContext>, IUserDapperRepository
    {
        public UserDapperRepository(IDbContextProvider<CrackerAdminDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Guid[]> GetSuperAdminUserIdsAsync()
        {
            var conn = await GetDbConnectionAsync();
            return conn.Query<Guid>("select u.id from sys_user u inner join sys_user_role ur on " +
                "ur.user_id=u.id inner join sys_role r on r.id=ur.role_id where u.is_deleted=0 and " +
                "r.role_name = @name", new { name = AdminConsts.SuperAdminRole }).ToArray();
        }
    }
}