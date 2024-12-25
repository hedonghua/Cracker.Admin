namespace Cracker.Admin.Helpers
{
    public class UserCacheHelper
    {
        /// <summary>
        /// 用户信息key匹配
        /// </summary>
        public const string UserInfoKeyPattern = "userinfo_*";

        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string KEY_SUPER_ADMIN = "superadmin";

        /// <summary>
        /// 用户信息key
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetUserInfoKey(Guid uid) => $"userinfo_{uid}";

        /// <summary>
        /// 在线用户
        /// </summary>
        public const string KEY_ONLINE_USERS = "online_users";
    }
}