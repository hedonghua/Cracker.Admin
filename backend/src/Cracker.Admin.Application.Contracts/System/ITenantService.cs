using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.System
{
    public interface ITenantService
    {
        Task AddTenantAsync(TenantDto dto);

        Task<PagedResultStruct<TenantResultDto>> GetTenantListAsync(TenantSearchDto dto);

        Task UpdateTenantAsync(TenantDto dto);

        Task DeleteTenantAsync(Guid tenantId);
    }
}