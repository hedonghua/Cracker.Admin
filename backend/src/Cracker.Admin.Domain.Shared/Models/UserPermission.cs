namespace Cracker.Admin.Models
{
    public class UserPermission
    {
        public Guid UserId { get; set; }

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
    }
}