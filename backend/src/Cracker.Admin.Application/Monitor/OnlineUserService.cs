using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Cracker.Admin.Core;
using Cracker.Admin.Entities;
using Cracker.Admin.Models;
using Cracker.Admin.Monitor.Dtos;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Monitor
{
    public class OnlineUserService : ApplicationService, IOnlineUserService
    {
        private readonly IRepository<SysLoginLog> loginLogRepository;
        private readonly ICacheProvider cacheProvider;
        private readonly IRepository<SysUser> userRepository;

        public OnlineUserService(IRepository<SysLoginLog> loginLogRepository, ICacheProvider cacheProvider, IRepository<SysUser> userRepository)
        {
            this.loginLogRepository = loginLogRepository;
            this.cacheProvider = cacheProvider;
            this.userRepository = userRepository;
        }

        public async Task<PagedResultStruct<OnlineUserResultDto>> GetOnlineUserListAsync(OnlineUserSearchDto dto)
        {
            //token时间4小时，1分钟误差
            var time = DateTime.Now.AddHours(-4).AddMinutes(-1);

            var loginLogs = (await loginLogRepository.GetQueryableAsync()).Where(x => x.CreationTime >= time)
                .Where(x => x.IsSuccess)
                .WhereIf(!string.IsNullOrEmpty(dto.UserName), x => x.UserName.Contains(dto.UserName!))
                .GroupBy(x => x.UserName).Select(g => g.OrderByDescending(x => x.CreationTime).First()).ToList();
            var userNames = loginLogs.Select(x => x.UserName).ToList();
            var users = (await userRepository.GetQueryableAsync()).Where(x => userNames.Contains(x.UserName)).Select(x => new { x.Id, x.UserName }).ToList();

            var list = new List<OnlineUserResultDto>();
            foreach (var loginLog in loginLogs)
            {
                var user = users.FirstOrDefault(x => x.UserName == loginLog.UserName);
                if (user == null) continue;

                if (await cacheProvider.ExistsAsync("AccessToken:" + user.Id))
                {
                    list.Add(new OnlineUserResultDto
                    {
                        UserId = user.Id,
                        UserName = loginLog.UserName,
                        Ip = loginLog.Ip,
                        Address = loginLog.Address,
                        Os = loginLog.Os,
                        Browser = loginLog.Browser,
                        CreationTime = loginLog.CreationTime
                    });
                }
            }
            var total = list.Count;

            return new PagedResultStruct<OnlineUserResultDto>(dto)
            {
                TotalCount = total,
                Items = list.OrderByDescending(s=>s.CreationTime).Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList()
            };
        }

        public async Task LogoutAsync(string userId)
        {
            //移除访问token
            await cacheProvider.DelAsync("AccessToken:" + userId);
            //移除刷新token
            await cacheProvider.DelAsync("RefreshToken:" + userId);
        }
    }
}