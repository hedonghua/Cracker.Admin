using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Cracker.Admin.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cracker.Admin.Core;

namespace Cracker.Admin.Filters
{
    public partial class AppGlobalActionFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AppGlobalActionFilter> _logger;
        private readonly ICacheProvider cacheProvider;

        public AppGlobalActionFilter(IConfiguration configuration, ILogger<AppGlobalActionFilter> logger,ICacheProvider cacheProvider)
        {
            _configuration = configuration;
            _logger = logger;
            this.cacheProvider = cacheProvider;
        }

        [GeneratedRegex("[\"\'\\\\/]+")]
        private static partial Regex MyRegex();

        private static string GetKeyPath(string path) => "rate_limit||" + MyRegex().Replace(path, "_").Trim('_');

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var rateLimitRs = await IsPassed(context.HttpContext);
            if (!rateLimitRs.IsOk())
            {
                context.Result = new ObjectResult(rateLimitRs);
                return;
            }
            var demoRs = IsDemonstrationMode(context.HttpContext);
            if(!demoRs.IsOk())
            {
                context.Result = new ObjectResult(demoRs);
                return;
            }

            await next();
        }

        /// <summary>
        /// 是否通过限流
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<AppResult> IsPassed(HttpContext context)
        {
            var isEnabled = bool.Parse(_configuration["ApiRateLimit:Enabled"]!);
            var timeSeconds = int.Parse(_configuration["ApiRateLimit:TimeSeconds"]!);
            var count = int.Parse(_configuration["ApiRateLimit:Count"]!);
            var whiteListPatterns = _configuration.GetValue<string[]>("ApiRateLimit:WhiteListPatterns");

            var requestPath = context.Request.Path.Value;
            var httpMethod = context.Request.Method.ToLower();
            //get请求不做限流处理
            if (isEnabled && !string.IsNullOrWhiteSpace(requestPath) && !IsIgnoreHttpMethod(httpMethod))
            {
                //白名单匹配
                if (whiteListPatterns != null)
                {
                    for (int i = 0; i < whiteListPatterns.Length; i++)
                    {
                        if (Regex.IsMatch(requestPath, whiteListPatterns[i])) return new AppResult();
                    }
                }
                var key = GetKeyPath(requestPath);
                if (await cacheProvider.ExistsAsync(key))
                {
                    int realCount = await cacheProvider.GetAsync<int>(key);
                    _logger.LogInformation("实际请求数：{realCount}", realCount);
                    if (realCount >= count)
                    {
                        return new AppResult(-1, "操作过快，请等一下");
                    }
                    realCount += 1;
                    await cacheProvider.IncrByAsync(key, 1);
                }
                else
                {
                    await cacheProvider.SetAsync(key, 1, TimeSpan.FromSeconds(timeSeconds));
                }
            }
            return new AppResult();
        }

        /// <summary>
        /// 是否演示模式
        /// </summary>
        /// <returns></returns>
        private AppResult IsDemonstrationMode(HttpContext context)
        {
            var isEnabled = bool.Parse(_configuration["App:DemonstrationMode"]!);
            var httpMethod = context.Request.Method.ToLower();
            var requestPath = context.Request.Path.Value?.ToLower();
            var isWhite = !string.IsNullOrWhiteSpace(requestPath) && (requestPath.Contains("account/login") || requestPath.Contains("account/signout"));

            if (isEnabled && !IsIgnoreHttpMethod(httpMethod) && !isWhite) return new AppResult(-1, "演示模式，不允许操作");
            return new AppResult();
        }

        /// <summary>
        /// 是否忽略http方式
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private bool IsIgnoreHttpMethod(string method) => method == "get" || method == "option";
    }
}