using Cracker.Admin.Entities;
using Cracker.Admin.System.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System
{
    public class TenantService : ApplicationService, ITenantService
    {
        private readonly IRepository<SysTenant> tenantRepository;

        public TenantService(IRepository<SysTenant> tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public async Task AddTenantAsync(TenantDto dto)
        {
            var entity = ObjectMapper.Map<TenantDto, SysTenant>(dto);
            await tenantRepository.InsertAsync(entity);

            //自动迁移数据库结构和数据

        }
    }
}