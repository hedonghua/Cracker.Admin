using Cracker.Admin.Models;

using Dapper;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin.Repositories
{
    public class RoleDapperRepository : DapperRepository<CrackerAdminDbContext>, IRoleDapperRepository
    {
        public RoleDapperRepository(IDbContextProvider<CrackerAdminDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<AppOption>> GetRoleOptionsAsync()
        {
            var conn = await GetDbConnectionAsync();
            return conn.Query<AppOption>("select convert(id,char) as Value,role_name as label from sys_role where is_deleted=0 and role_name != @name", new { name = AdminConsts.SUPERADMIN }).ToList();
        }
    }
}