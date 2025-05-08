using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.Account.Dtos
{
    public class UserPwdDto
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [NotNull]
        [Required]
        public string? OldPwd { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [NotNull]
        [Required]
        [MinLength(6)]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z\\d@_\\-\\.]{6,16}$")]
        public string? NewPwd { get; set; }
    }
}