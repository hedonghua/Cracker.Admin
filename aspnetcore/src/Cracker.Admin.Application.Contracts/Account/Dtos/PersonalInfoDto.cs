using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.Account.Dtos
{
    public class PersonalInfoDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        public string? Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string? NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public int Sex { get; set; }
    }
}