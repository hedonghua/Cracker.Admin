using Cracker.Admin.Entities;
using Cracker.Admin.Helpers;
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
        private readonly IRepository<SysTenant, string> tenantRepository;
        private readonly IBackgroundJobManager backgroundJobManager;

        public TenantService(IRepository<SysTenant, string> tenantRepository, IBackgroundJobManager backgroundJobManager)
        {
            this.tenantRepository = tenantRepository;
            this.backgroundJobManager = backgroundJobManager;
        }

        public async Task AddTenantAsync(TenantDto dto)
        {
            var entity = new SysTenant(await this.GetIdAsync())
            {
                Name = dto.Name,
                ConnectionString = dto.ConnectionString,
                RedisConnection = dto.RedisConnection,
                Remark = dto.Remark,
            };
            await tenantRepository.InsertAsync(entity);

            await backgroundJobManager.EnqueueAsync(new CreationDbParameter { ConnectionString = dto.ConnectionString });
        }

        public async Task<string> GetIdAsync()
        {
            var code = StringHelper.RandomStr(6, true);
            if (await tenantRepository.AnyAsync(x => x.Id == code))
            {
                return await this.GetIdAsync();
            }
            return code;
        }
    }
}