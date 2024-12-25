using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cracker.Admin.Helpers
{
    public class TokenHelper
    {
        public static string GenerateToken(IEnumerable<Claim> claims, DateTime expireTime)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppManager.Configuration.GetSection("JWT")["IssuerSigningKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: AppManager.Configuration.GetSection("JWT")["ValidIssuer"],
                audience: AppManager.Configuration.GetSection("JWT")["ValidAudience"],
                claims: claims,
                expires: expireTime,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        /// <summary>
        /// 从Token中获取用户身份
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal? GetPrincipalFromAccessToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppManager.Configuration.GetSection("JWT")["IssuerSigningKey"]!));
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}