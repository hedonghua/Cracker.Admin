using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

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
            if (await tenantRepository.AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower()))
            {
                throw new AbpValidationException($"租户[{dto.Name}]已存在");
            }

            var entity = new SysTenant()
            {
                Name = dto.Name,
                ConnectionString = RSAEncrypt(dto.ConnectionString!),
                RedisConnection = RSAEncrypt(dto.RedisConnection!),
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
                .WhereIf(!string.IsNullOrEmpty(dto.Name), x => x.Name.Contains(dto.Name!))
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

            var nameLower = dto.Name.ToLower();
            if (await tenantRepository.AnyAsync(x => x.Name.ToLower() == nameLower) && !nameLower.Equals(entity.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new AbpValidationException($"租户[{dto.Name}]已存在");
            }

            entity.Name = dto.Name;
            entity.ConnectionString = RSAEncrypt(dto.ConnectionString!);
            entity.RedisConnection = RSAEncrypt(dto.RedisConnection!);
            entity.Remark = dto.Remark;

            await tenantRepository.UpdateAsync(entity);
        }

        private string RSAEncrypt(string str)
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RSAKeys");
            var publicKeysPath = Path.Combine(dir, "PublicKeys.txt");
            return EncryptionHelper.RSAEncrypt(str, File.ReadAllText(publicKeysPath));
        }
    }
}