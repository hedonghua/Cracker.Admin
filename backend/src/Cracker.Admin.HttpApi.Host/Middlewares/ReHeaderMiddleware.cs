using System.Threading.Tasks;

using IP2Region.Net.XDB;

using Microsoft.AspNetCore.Http;

using Cracker.Admin.Core;

namespace Cracker.Admin.Middlewares
{
    public class ReHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public ReHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            var header = context.Request.Headers;
            var parser = UAParser.Parser.GetDefault().Parse(header.UserAgent);
            string? ip;
            if (header.TryGetValue("X-Real-IP", out var val1))
            {
                ip = val1;
            }
            else if (header.TryGetValue("X-Forwarded-For", out var val2))
            {
                ip = val2;
            }
            else
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }
            //拿到地址示例：中国|0|重庆|重庆市|移动
            var address = ResolveAddress(new Searcher(CachePolicy.Content, "ip2region.xdb").Search(ip!));

            var reHeader = new ReHeader(context, ip, address, parser.OS.Family, path, method, parser.UA.Family);
            context.Features.Set(reHeader);
            await _next(context);
        }

        private static string ResolveAddress(string? address)
        {
            if (string.IsNullOrWhiteSpace(address)) return string.Empty;
            if (address.Contains("0|0|0"))
            {
                return "未知";
            }
            string[] strs = address.Split('|');
            if (strs.Length >= 4)
            {
                return string.Concat(strs[0], strs[2], strs[3]);
            }
            else if (strs.Length == 1)
            {
                return strs[0];
            }
            return string.Empty;
        }
    }
}