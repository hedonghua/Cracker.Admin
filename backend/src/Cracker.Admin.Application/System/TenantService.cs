using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.Linq;
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

        public async Task DeleteTenantAsync(Guid tenantId)
        {
            await tenantRepository.DeleteAsync(x => x.Id == tenantId);
        }

        public async Task<PagedResultStruct<TenantResultDto>> GetTenantListAsync(TenantSearchDto dto)
        {
            var query = (await tenantRepository.GetQueryableAsync())
                .Select(x => new TenantResultDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ConnectionString = x.ConnectionString,
                    RedisConnection = x.RedisConnection,
                    Remark = x.Remark
                });
            return new PagedResultStruct<TenantResultDto>(dto)
            {
                TotalCount = query.Count(),
                Items = query.StartPage(dto).ToList()
            };
        }

        public async Task UpdateTenantAsync(TenantDto dto)
        {
            var entity = await tenantRepository.GetAsync(x => x.Id == dto.Id);
            entity.Name = dto.Name;
            entity.ConnectionString = dto.ConnectionString;
            entity.RedisConnection = dto.RedisConnection;
            entity.Remark = dto.Remark;

            await tenantRepository.UpdateAsync(entity);
        }
    }
}