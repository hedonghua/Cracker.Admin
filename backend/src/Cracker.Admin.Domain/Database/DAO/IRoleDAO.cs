using System.Collections.Generic;
using System.Threading.Tasks;
using Cracker.Admin.Models;

using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Database.DAO
{
    public interface IRoleDAO : ITransientDependency
    {
        Task<List<AppOption>> GetRoleOptionsAsync();
    }
}