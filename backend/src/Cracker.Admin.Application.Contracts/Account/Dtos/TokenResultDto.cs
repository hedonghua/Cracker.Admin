using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cracker.Admin.Account.Dtos
{
    public class TokenResultDto
    {
        /// <summary>
        /// 访问token
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// 过期时间，格式YYYY-MM-DD HH:mm:ss
        /// </summary>
        public string? ExpiredTime { get; set; }
    }
}
