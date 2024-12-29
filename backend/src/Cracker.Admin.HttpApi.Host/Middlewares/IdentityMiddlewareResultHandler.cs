using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cracker.Admin.Middlewares
{
    public class IdentityMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();
        private readonly IdentityDomainService identityDomainService;

        public IdentityMiddlewareResultHandler(IdentityDomainService identityDomainService)
        {
            this.identityDomainService = identityDomainService;
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                //身份验证
                var subjectId = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                var requestToken = context.Request.Headers["Authorization"].ToString().Replace(JwtBearerDefaults.AuthenticationScheme, "").Trim();
                var isValid = await identityDomainService.CheckTokenAsync(subjectId!, requestToken);
                if (!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new AppResult(AdminResponseCode.NoAuth, "身份信息已过期，请重新登录"));
                    return;
                }

                //检查权限
                var auth = context.GetEndpoint()?.Metadata.GetMetadata<HasPermissionAttribute>();
                if (auth != null)
                {
                    var isPass = await identityDomainService.CheckPermissionAsync(subjectId!, auth.Code);
                    if (!isPass)
                    {
                        await context.Response.WriteAsJsonAsync(new AppResult(AdminResponseCode.Forbidden, "权限不足，请联系管理员"));
                        return;
                    }
                }
            }

            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}