using Cracker.Admin.Entities;
using Cracker.Admin.System.LogManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System.LogManagement
{
    public class LoginLogService : ApplicationService, ILoginLogService
    {
        private readonly IRepository<SysLoginLog> _loginLogRepository;

        public LoginLogService(IRepository<SysLoginLog> loginLogRepository)
        {
            _loginLogRepository = loginLogRepository;
        }

        public async Task<bool> DeleteLoginLogsAsync(long[] ids)
        {
            await _loginLogRepository.DeleteDirectAsync(x => ids.Contains(x.Id));
            return true;
        }

        public async Task<PagedResultDto<LoginLogListDto>> GetLoginLogListAsync(LoginLogQueryDto dto)
        {
            var query = (await _loginLogRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.UserName), x => x.UserName.Contains(dto.UserName!))
                .WhereIf(dto.Status == 1, x => x.IsSuccess)
                .WhereIf(dto.Status == 2, x => !x.IsSuccess)
                .WhereIf(!string.IsNullOrEmpty(dto.Address), x => x.Address != null && x.Address.Contains(dto.Address!))
                .WhereIf(!string.IsNullOrEmpty(dto.Os), x => x.Os != null && x.Os.Contains(dto.Os!));
            var count = query.Count();
            var rows = query.OrderByDescending(x => x.CreationTime).Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return new PagedResultDto<LoginLogListDto>(count, ObjectMapper.Map<List<SysLoginLog>, List<LoginLogListDto>>(rows));
        }
    }
}