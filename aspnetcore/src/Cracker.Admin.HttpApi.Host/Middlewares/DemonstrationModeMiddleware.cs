using Cracker.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Cracker.Admin.Middlewares
{
    public class DemonstrationModeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;

        public DemonstrationModeMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var demoRs = IsDemonstrationMode(context);
            if (!demoRs.IsOk())
            {
                await context.Response.WriteAsJsonAsync(demoRs);
                return;
            }

            await next(context);
        }

        /// <summary>
        /// 是否演示模式
        /// </summary>
        /// <returns></returns>
        private AppResponse IsDemonstrationMode(HttpContext context)
        {
            var isEnabled = bool.Parse(_configuration["App:DemonstrationMode"]!);
            var httpMethod = context.Request.Method.ToLower();
            var requestPath = context.Request.Path.Value?.ToLower();
            var isWhite = !string.IsNullOrWhiteSpace(requestPath) && (requestPath.Contains("account/login") || requestPath.Contains("account/signout"));

            if (isEnabled && !IsIgnoreHttpMethod(httpMethod) && !isWhite) return new AppResponse(-1, "演示模式，不允许操作");
            return new AppResponse();
        }

        /// <summary>
        /// 是否忽略http
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private bool IsIgnoreHttpMethod(string method) => method == "get" || method == "option";
    }
}