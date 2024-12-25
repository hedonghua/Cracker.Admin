using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Cracker.Admin.Models;
using System;

namespace Cracker.Admin.Filters
{
    /// <summary>
    /// 程序返回统一结果
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AppResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            if (ctx.Result is ObjectResult rs)
            {
                if (rs.Value is bool v)
                {
                    setResult(new AppResult(v));
                }
                else
                {
                    setResult(new AppResult<object>(rs.Value));
                }
            }

            void setResult(object? value)
            {
                ctx.Result = new ObjectResult(value);
            }
        }
    }
}