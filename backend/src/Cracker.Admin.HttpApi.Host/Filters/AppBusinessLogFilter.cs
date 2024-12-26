using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using Cracker.Admin.Helpers;
using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;
using Cracker.Admin.Core;

namespace Cracker.Admin.Filters
{
    public class AppBusinessLogFilter : ActionFilterAttribute
    {
        public readonly string _node;

        public AppBusinessLogFilter(string node)
        {
            _node = node;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? action = context.ActionDescriptor.DisplayName;
            long timestamp = TimeHelper.GetCurrentTimestamp();
            context.HttpContext.Items.Add("action", action);
            context.HttpContext.Items.Add("timestamp", timestamp);
            context.HttpContext.Items.Add("node", _node);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            WriteLog(context.HttpContext, context.Result);
        }

        /// <summary>
        /// 异常时不会走<see cref="OnResultExecuted"/>，所以抽离，让异常过滤器写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        public static void WriteLog(HttpContext context, IActionResult result)
        {
            var items = context.Items;
            if (!items.ContainsKey("node")) return;
            var ts = TimeHelper.GetCurrentTimestamp();
            var beforeTs = (long)items["timestamp"]!;
            var service = AppManager.ServiceProvider.GetRequiredService<IBusinessLogService>();
            var model = new BusinessLogDto
            {
                Action = (string?)items["action"],
                MillSeconds = (int)(ts - beforeTs),
                NodeName = (string?)items["node"]
            };
            if (result is ObjectResult objRes && objRes.Value is IAppResult res)
            {
                model.IsSuccess = res.IsOk();
                model.OperationMsg = res.Message;
            }
            context.Items.Remove("action");
            context.Items.Remove("timestamp");
            context.Items.Remove("node");
            service.AddBusinessLogAsync(model).Wait(100);
        }
    }
}