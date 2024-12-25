using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mapster;

using MediatR;

using Cracker.Admin.Core;
using Cracker.Admin.Database.DAO;
using Cracker.Admin.Entity;
using Cracker.Admin.System.LogManagement.Commands;
using Cracker.Admin.System.LogManagement.Dtos;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System.LogManagement
{
    public class LoginLogService : ApplicationService, ILoginLogService, IRequestHandler<AddLoginLogCommand, bool>
    {
        private readonly IRepository<SysLoginLog> _loginLogRepository;
        private readonly IReHeader _reHeader;
        private readonly ILoginLogDAO _loginLogDAO;

        public LoginLogService(IRepository<SysLoginLog> loginLogRepository, IReHeader reHeader, ILoginLogDAO loginLogDAO)
        {
            _loginLogRepository = loginLogRepository;
            _reHeader = reHeader;
            _loginLogDAO = loginLogDAO;
        }

        public async Task<bool> DeleteLoginLogsAsync(int[] ids)
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

        public async Task<bool> Handle(AddLoginLogCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<SysLoginLog>();
            entity.Os = _reHeader.Os;
            entity.Address = _reHeader.Address;
            entity.Ip = _reHeader.Ip;
            entity.Browser = _reHeader.Browser;
            await _loginLogDAO.WriteAsync(entity);
            return true;
        }
    }
}