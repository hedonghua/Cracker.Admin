using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? NewPwd { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [NotNull]
        [Required]
        public string? SurePwd { get; set; }
    }
}