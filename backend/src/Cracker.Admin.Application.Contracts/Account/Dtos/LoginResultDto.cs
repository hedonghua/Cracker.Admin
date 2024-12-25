using System.Text.Json.Serialization;

namespace Cracker.Admin.Account.Dtos
{
    public class LoginResultDto: TokenResultDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string[]? Auths { get; set; }
    }
}
