using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class ResetUserPwdDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MinLength(6)]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z\\d@_\\-\\.]{6,16}$")]
        public string? Password { get; set; }
    }
}