using Cracker.Admin.Core;
using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Local;

namespace Cracker.Admin.Filters
{
    public class AppBusinessLogFilter : ActionFilterAttribute
    {
        public readonly string _node;

        public AppBusinessLogFilter(string node)
        {
            _node = node;
        }

        public string Node => _node;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            var model = new BusinessLogDto
            {
                Action = context.ActionDescriptor.DisplayName,
                MillSeconds = 0,
                NodeName = _node,
                RequestId = Activity.Current?.TraceId.ToString(),
                IsSuccess = true,
            };
            if (context.Result is ObjectResult objRes && objRes.Value is IAppResponse res)
            {
                model.IsSuccess = res.IsOk();
                model.OperationMsg = res.Message;
            }

            var eventBus = context.GetService<ILocalEventBus>();
            if (eventBus != null) await eventBus.PublishAsync(model);
        }
    }
}