using System.Threading.Tasks;

using Cracker.Admin.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Cracker.Admin.Filters
{
    public class AppGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<AppGlobalExceptionFilter> _logger;

        public AppGlobalExceptionFilter(ILogger<AppGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled) return;

            var errMsg = context.Exception.Message;
            var result = new AppResult(AdminResponseCode.Fail, errMsg);

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
            else if (context.Exception is EntityNotFoundException)
            {
                result.Message = "数据不存在";
            }

            if (errMsg.Contains("There is no such an entity given id"))
            {
                result.Message = "数据不存在";
            }

            context.Result = new ObjectResult(result);
            context.ExceptionHandled = true;

            //AppBusinessLogFilter.WriteLog(context.HttpContext, context.Result);

            if (context.Exception is AbpValidationException
                || context.Exception is BusinessException) return;

            _logger.LogError(context.Exception, "全局捕获异常");
        }
    }
}