using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using Cracker.Admin.Database.DAO;
using Cracker.Admin.EntitiesFrameworkCore;
using Cracker.Admin.Models;

using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Cracker.Admin.DAO
{
    public class RoleDAO : DapperRepository<CrackerAdminDbContext>, IRoleDAO
    {
        public RoleDAO(IDbContextProvider<CrackerAdminDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<AppOption>> GetRoleOptionsAsync()
        {
            var conn = await GetDbConnectionAsync();
            return conn.Query<AppOption>("select convert(id,char) as Value,role_name as label from sys_role where is_deleted=0 and role_name != @name",new {name = AdminConsts.SUPERADMIN}).ToList();
        }
    }
}