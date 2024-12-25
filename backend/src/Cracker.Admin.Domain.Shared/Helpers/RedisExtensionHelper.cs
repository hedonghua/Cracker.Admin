namespace Cracker.Admin.Helpers
{
    /// <summary>
    /// redishelper增强功能
    /// </summary>
    public class RedisExtensionHelper
    {
        /// <summary>
        /// 正则移除key
        /// </summary>
        /// <param name="pattern"></param>
        public static void RemoveByPattern(string pattern) 
        {
            var keys = RedisHelper.Keys(pattern);
            foreach (var key in keys)
            {
                RedisHelper.Del(key);
            }
        }

        /// <summary>
        /// 移除hash表所有字段
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveHKeys(string key)
        {
            var fields = RedisHelper.HKeys(key);
            RedisHelper.HDel(key,fields);
        }
    }
}