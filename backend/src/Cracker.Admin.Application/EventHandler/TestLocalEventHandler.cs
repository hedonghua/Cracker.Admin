using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Cracker.Admin.EventHandler
{
    public class TestLocalEventHandler : ILocalEventHandler<LoginLogQueryDto>, ITransientDependency
    {
        private readonly ILogger<TestLocalEventHandler> logger;

        public TestLocalEventHandler(ILogger<TestLocalEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task HandleEventAsync(LoginLogQueryDto eventData)
        {
            logger.LogInformation("接收到登录日志查询参数：{@eventData}", eventData);
        }
    }
}