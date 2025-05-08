using Cracker.Admin.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;

namespace Cracker.Admin.EventHandlers
{
    public class WriteLoginLogLocalEventHandler : ILocalEventHandler<SysLoginLog>, ITransientDependency
    {
        private readonly ILogger<WriteLoginLogLocalEventHandler> logger;
        private readonly IRepository<SysLoginLog> loginLogRepository;

        public WriteLoginLogLocalEventHandler(ILogger<WriteLoginLogLocalEventHandler> logger, IRepository<SysLoginLog> loginLogRepository)
        {
            this.logger = logger;
            this.loginLogRepository = loginLogRepository;
        }

        public async Task HandleEventAsync(SysLoginLog eventData)
        {
            await loginLogRepository.InsertAsync(eventData);
        }
    }
}