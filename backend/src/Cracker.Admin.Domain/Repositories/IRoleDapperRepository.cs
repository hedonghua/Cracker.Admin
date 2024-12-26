using Cracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.Dapper;

namespace Cracker.Admin.Repositories
{
    public interface IRoleDapperRepository : IDapperRepository
    {
        Task<List<AppOption>> GetRoleOptionsAsync();
    }
}