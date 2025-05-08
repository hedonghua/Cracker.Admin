using Cracker.Admin.Entities;
using Cracker.Admin.Models;
using Cracker.Admin.Monitor.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Monitor
{
    public class OnlineUserService : ApplicationService, IOnlineUserService
    {
        private readonly IRepository<SysLoginLog> loginLogRepository;
        private readonly IDatabase redisDb;
        private readonly IRepository<SysUser> userRepository;

        public OnlineUserService(IRepository<SysLoginLog> loginLogRepository, IDatabase redisDb, IRepository<SysUser> userRepository)
        {
            this.loginLogRepository = loginLogRepository;
            this.redisDb = redisDb;
            this.userRepository = userRepository;
        }

        public async Task<PagedResultStruct<OnlineUserResultDto>> GetOnlineUserListAsync(OnlineUserSearchDto dto)
        {
            //token时间4小时，1分钟误差
            var time = DateTime.Now.AddHours(-4).AddMinutes(-1);

            var loginLogs = (await loginLogRepository.GetQueryableAsync()).Where(x => x.CreationTime >= time)
                .Where(x => x.IsSuccess && !string.IsNullOrEmpty(x.SessionId))
                .WhereIf(!string.IsNullOrEmpty(dto.UserName), x => x.UserName.Contains(dto.UserName!))
                .OrderByDescending(x => x.CreationTime).ToList();
            var userNames = loginLogs.Select(x => x.UserName).ToList();
            var users = (await userRepository.GetQueryableAsync()).Where(x => userNames.Contains(x.UserName)).Select(x => new { x.Id, x.UserName }).ToList();

            var list = new List<OnlineUserResultDto>();
            foreach (var loginLog in loginLogs)
            {
                var user = users.FirstOrDefault(x => x.UserName == loginLog.UserName);
                if (user == null) continue;

                if (await redisDb.KeyExistsAsync($"AccessToken:{user.Id}:{loginLog.SessionId}"))
                {
                    list.Add(new OnlineUserResultDto
                    {
                        UserId = user.Id,
                        UserName = loginLog.UserName,
                        Ip = loginLog.Ip,
                        Address = loginLog.Address,
                        Os = loginLog.Os,
                        Browser = loginLog.Browser,
                        CreationTime = loginLog.CreationTime,
                        SessionId = loginLog.SessionId
                    });
                }
            }
            var total = list.Count;

            return new PagedResultStruct<OnlineUserResultDto>(dto)
            {
                TotalCount = total,
                Items = list.OrderByDescending(s => s.CreationTime).Skip((dto.Current - 1) * dto.PageSize).Take(dto.PageSize).ToList()
            };
        }

        public async Task LogoutAsync(string key)
        {
            //移除访问token
            await redisDb.KeyDeleteAsync("AccessToken:" + key);
            //移除刷新token
            await redisDb.KeyDeleteAsync("RefreshToken:" + key);
        }
    }
}