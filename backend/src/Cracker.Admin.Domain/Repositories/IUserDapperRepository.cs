using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories.Dapper;

namespace Cracker.Admin.Repositories
{
    public interface IUserDapperRepository : IDapperRepository
    {
        Task<Guid[]> GetSuperAdminUserIdsAsync();
    }
}