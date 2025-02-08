using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Jobs
{
    public class TestJob : IInvocable, ITransientDependency
    {
        private readonly ILogger<TestJob> logger;

        public TestJob(ILogger<TestJob> logger)
        {
            this.logger = logger;
        }

        public async Task Invoke()
        {
            await Task.Delay(50);
            logger.LogInformation("【测试】定时任务");
        }
    }
}