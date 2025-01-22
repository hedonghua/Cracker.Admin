using Cracker.Admin.System.Dtos;
using System.Threading.Tasks;

namespace Cracker.Admin.System
{
    public interface ITenantService
    {
        Task AddTenantAsync(TenantDto dto);
    }
}