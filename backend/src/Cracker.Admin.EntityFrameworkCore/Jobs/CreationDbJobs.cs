using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Jobs
{
    public class CreationDbJobs : AsyncBackgroundJob<CreationDbParameter>, ITransientDependency
    {
        private readonly ILogger<CreationDbJobs> logger;
        private readonly MqttService mqttService;

        public CreationDbJobs(ILogger<CreationDbJobs> logger, MqttService mqttService)
        {
            this.logger = logger;
            this.mqttService = mqttService;
        }

        public override async Task ExecuteAsync(CreationDbParameter args)
        {
            //结构迁移
            var context = CrackerAdminDbContextFactory.CreateDbContext(args.ConnectionString);
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("数据库创建成功：{ConnectionString}", args.ConnectionString);

            //初始数据
            var rootDir = Path.Combine("SqlData");
            var filePaths = Directory.GetFiles(rootDir);
            foreach (var filePath in filePaths)
            {
                try
                {
                    var sql = await File.ReadAllTextAsync(filePath);
                    await context.Database.GetDbConnection().ExecuteAsync(sql);
                    logger.LogInformation("{filePath}执行成功", filePath);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{filePath}执行失败", filePath);
                }
            }

            await mqttService.PushAsync("notification", new NotificationBody
            {
                Type = "success",
                Message = $"租户[{args.Name}]数据库创建成功"
            });
        }
    }
}