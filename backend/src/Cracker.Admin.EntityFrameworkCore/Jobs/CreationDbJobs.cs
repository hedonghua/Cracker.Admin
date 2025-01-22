using Cracker.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Jobs
{
    public class CreationDbJobs : AsyncBackgroundJob<CreationDbParameter>, ITransientDependency
    {
        private readonly ILogger<CreationDbJobs> logger;

        public CreationDbJobs(ILogger<CreationDbJobs> logger)
        {
            this.logger = logger;
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
                var sql = await File.ReadAllTextAsync(filePath);
                await context.Database.ExecuteSqlRawAsync(sql);
                logger.LogInformation("{filePath}执行成功", filePath);
            }
        }
    }
}