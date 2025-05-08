using Coravel.Invocable;
using Cracker.Admin.Entities;
using Cracker.Admin.Services;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Jobs
{
    public class NotificationJob : IInvocable, ITransientDependency
    {
        private readonly ILogger<NotificationJob> logger;
        private readonly IRepository<Notification> repository;
        private readonly MqttService mqttService;
        private readonly IDatabase database;

        public NotificationJob(ILogger<NotificationJob> logger, IRepository<Notification> repository, MqttService mqttService, IDatabase database)
        {
            this.logger = logger;
            this.repository = repository;
            this.mqttService = mqttService;
            this.database = database;
        }

        public async Task Invoke()
        {
            try
            {
                var lockSucc = await database.LockTakeAsync(nameof(NotificationJob), "1", TimeSpan.FromMilliseconds(500));
                if (lockSucc)
                {
                    var notis = await repository.GetListAsync(x => !x.IsReaded);
                    var groupMap = notis.GroupBy(x => x.EmployeeId).ToDictionary(k => k.Key, v => v.Count());
                    var random = new Random();
                    if (notis.Count > 0)
                    {
                        foreach (var g in groupMap)
                        {
                            var curEmployeeNotis = notis.Where(x => x.EmployeeId == g.Key).ToList();
                            var index = random.Next(0, curEmployeeNotis.Count);
                            var item = curEmployeeNotis[index];
                            var lastNotiKey = "LastNotification" + item.EmployeeId;
                            if (await database.KeyExistsAsync(lastNotiKey))
                            {
                                var lastNotiId = await database.StringGetAsync(lastNotiKey);
                                //随机取到了上条通知，就往后取一条
                                if (lastNotiId == item.Id.ToString() && curEmployeeNotis.Count > 1)
                                {
                                    if (index < curEmployeeNotis.Count - 1)
                                    {
                                        item = curEmployeeNotis[index + 1];
                                    }
                                }
                            }
                            var isSuc = await mqttService.PushAsync("Notification:" + item.EmployeeId, new { title = item.Title, description = item.Description, NoReadedCount = g.Value });
                            if (!isSuc) continue;
                            //上条通知的ID
                            await database.StringSetAsync("LastNotification" + item.EmployeeId, item.Id.ToString(), TimeSpan.FromMinutes(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            finally
            {
                await database.LockReleaseAsync(nameof(NotificationJob), "1");
            }
        }
    }
}