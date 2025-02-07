using Cracker.Admin.Entities;
using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System
{
    public class TenantService : ApplicationService, ITenantService
    {
        private readonly IRepository<SysTenant> tenantRepository;
        private readonly IBackgroundJobManager backgroundJobManager;

        public TenantService(IRepository<SysTenant> tenantRepository, IBackgroundJobManager backgroundJobManager)
        {
            this.tenantRepository = tenantRepository;
            this.backgroundJobManager = backgroundJobManager;
        }

        public async Task AddTenantAsync(TenantDto dto)
        {
            var entity = new SysTenant()
            {
                Name = dto.Name,
                ConnectionString = dto.ConnectionString,
                RedisConnection = dto.RedisConnection,
                Remark = dto.Remark,
            };
            await tenantRepository.InsertAsync(entity);

            await backgroundJobManager.EnqueueAsync(new CreationDbParameter { ConnectionString = dto.ConnectionString });
        }
    }
}