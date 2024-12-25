using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Database.DAO
{
    public interface IUserDAO : ITransientDependency
    {
        Task<Guid[]> GetSuperAdminUserIdsAsync();
    }
}