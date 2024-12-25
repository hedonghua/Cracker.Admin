using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Cracker.Admin.Account.Dtos
{
    public class UserInfoDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string[]? Roles { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string[]? Auths { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid[]? RoleIds { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid[]? MenuIds { get; set; }

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
        public int Sex { get; set; }

        public bool IsSuperAdmin()
        {
            if (Roles == null) return false;
            return Roles.Contains(AdminConsts.SUPERADMIN);
        }
    }
}