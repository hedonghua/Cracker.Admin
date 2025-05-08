using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 用户名（大小写字母，数字，下划线，长度3-12位）
        /// </summary>
        [NotNull]
        [Required]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z0-9_]{3,12}$")]
        public string? UserName { get; set; }

        /// <summary>
        /// 密码 （至少有一个字母和数字，长度6-16位，特殊字符"-@_."）
        /// </summary>
        [NotNull]
        [Required]
        [MinLength(6)]
        [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z\\d@_\\-\\.]{6,16}$")]
        public string? Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [NotNull]
        [Required]
        public int Sex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }
    }
}