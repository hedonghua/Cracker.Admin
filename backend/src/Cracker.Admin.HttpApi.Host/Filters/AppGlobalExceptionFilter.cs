using Cracker.Admin.Models;
using Cracker.Admin.System.LogManagement;
using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Cracker.Admin.Filters
{
    public class AppGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<AppGlobalExceptionFilter> _logger;
        private readonly IBusinessLogService businessLogService;

        public AppGlobalExceptionFilter(ILogger<AppGlobalExceptionFilter> logger, IBusinessLogService businessLogService)
        {
            _logger = logger;
            this.businessLogService = businessLogService;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled) return;

            var errMsg = context.Exception.Message;
            var result = new AppResponse(AdminResponseCode.Fail, errMsg);

            if (context.Exception is BusinessException businessException)
            {
                if (int.TryParse(businessException.Code, out var code))
                {
                    result.Code = code;
                }
                else
                {
                    result.Status = businessException.Code;
                }
            }
            else if (context.Exception is EntityNotFoundException || errMsg.Contains("There is no such an entity given id"))
            {
                result.Message = "数据不存在";
            }

            context.Result = new ObjectResult(result);
            context.ExceptionHandled = true;

            var businessLogFilter = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType().Equals(typeof(AppBusinessLogFilter)));
            if (businessLogFilter is AppBusinessLogFilter businessLogFilterAttr)
            {
                await businessLogService.AddBusinessLogAsync(new BusinessLogDto
                {
                    Action = context.ActionDescriptor.DisplayName,
                    MillSeconds = 0,
                    NodeName = businessLogFilterAttr.Node,
                    RequestId = Activity.Current?.TraceId.ToString(),
                    IsSuccess = false,
                    OperationMsg = context.Exception.Message
                });
            }

            if (context.Exception is AbpValidationException
                || context.Exception is BusinessException) return;

            _logger.LogError(context.Exception, "全局捕获异常");
        }
    }
}