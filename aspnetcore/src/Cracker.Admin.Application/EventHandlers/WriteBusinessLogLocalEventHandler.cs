using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Cracker.Admin.EventHandlers
{
    public class WriteBusinessLogLocalEventHandler : ILocalEventHandler<BusinessLogDto>, ITransientDependency
    {
        private readonly IBusinessLogService businessLogService;

        public WriteBusinessLogLocalEventHandler(IBusinessLogService businessLogService)
        {
            this.businessLogService = businessLogService;
        }

        public async Task HandleEventAsync(BusinessLogDto eventData)
        {
            await businessLogService.AddBusinessLogAsync(eventData);
        }
    }
}