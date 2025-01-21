using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cracker.Admin.Middlewares
{
    public class ApiRateLimitMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiRateLimitMiddleware> _logger;
        private readonly IDatabase redisDb;

        public ApiRateLimitMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ApiRateLimitMiddleware> logger, IDatabase redisDb)
        {
            _configuration = configuration;
            _logger = logger;
            this.redisDb = redisDb;
            this.next = next;
        }

        private static string GetKeyPath(string path) => "rate_limit||" + new Regex("[\"\'\\\\/]+").Replace(path, "_").Trim('_');

        public async Task InvokeAsync(HttpContext context)
        {
            var rateLimitRs = await IsPassed(context);
            if (!rateLimitRs.IsOk())
            {
                await context.Response.WriteAsJsonAsync(rateLimitRs);
                return;
            }
            var demoRs = IsDemonstrationMode(context);
            if (!demoRs.IsOk())
            {
                await context.Response.WriteAsJsonAsync(demoRs);
                return;
            }

            await next(context);
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
                if (await redisDb.KeyExistsAsync(key))
                {
                    int realCount = int.Parse((await redisDb.StringGetAsync(key))!);
                    _logger.LogInformation("实际请求数：{realCount}", realCount);
                    if (realCount >= count)
                    {
                        return new AppResult(-1, "操作过快，请等一下");
                    }
                    await redisDb.StringIncrementAsync(key, 1);
                }
                else
                {
                    await redisDb.StringSetAsync(key, 1, TimeSpan.FromSeconds(timeSeconds));
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