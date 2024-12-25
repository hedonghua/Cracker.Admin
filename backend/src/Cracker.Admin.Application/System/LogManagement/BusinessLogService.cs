using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Mapster;

using Cracker.Admin.Core;
using Cracker.Admin.Entity;
using Cracker.Admin.System.LogManagement.Dtos;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Cracker.Admin.System.LogManagement
{
    public class BusinessLogService : ApplicationService, IBusinessLogService
    {
        private readonly IRepository<SysBusinessLog> _repository;
        private readonly IReHeader _reHeader;
        private readonly ICurrentUser _currentUser;

        public BusinessLogService(IRepository<SysBusinessLog> repository, IReHeader reHeader, ICurrentUser currentUser)
        {
            _repository = repository;
            _reHeader = reHeader;
            _currentUser = currentUser;
        }

        public async Task<bool> AddBusinessLogAsync(BusinessLogDto dto)
        {
            var entity = dto.Adapt<SysBusinessLog>();
            entity.Ip = _reHeader.Ip;
            entity.Address = _reHeader.Address;
            entity.Url = _reHeader.Url;
            entity.HttpMethod = _reHeader.HttpMethod;
            entity.UserName = _currentUser.UserName;
            entity.Os = _reHeader.Os;
            entity.Browser = _reHeader.Browser;
            var context = await _repository.GetDbContextAsync();
            await context.AddAsync(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBusinessLogsAsync(int[] ids)
        {
            await _repository.DeleteDirectAsync(x => ids.Contains(x.Id));
            return true;
        }

        public async Task<PagedResultDto<BusinessLogListDto>> GetBusinessLogListAsync(BusinessLogQueryDto dto)
        {
            var query = (await _repository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.UserName), x => x.UserName.Contains(dto.UserName!))
                .WhereIf(dto.Status == 1, x => x.IsSuccess)
                .WhereIf(dto.Status == 2, x => !x.IsSuccess)
                .WhereIf(!string.IsNullOrEmpty(dto.Action), x => x.Action != null && x.Action.Contains(dto.Action!))
                .WhereIf(dto.MillSeconds > 0, x => x.MillSeconds >= dto.MillSeconds)
                .WhereIf(!string.IsNullOrEmpty(dto.NodeName), x => x.NodeName != null && x.NodeName.Contains(dto.NodeName!));
            var count = query.Count();
            var rows = query.OrderByDescending(x => x.CreationTime).Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return new PagedResultDto<BusinessLogListDto>(count, ObjectMapper.Map<List<SysBusinessLog>, List<BusinessLogListDto>>(rows));
        }
    }
}