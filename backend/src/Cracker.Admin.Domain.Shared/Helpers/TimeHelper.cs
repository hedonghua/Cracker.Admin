namespace Cracker.Admin.Helpers
{
    public class TimeHelper
    {
        private static readonly DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// 获取当前时间戳（秒）
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimestamp()
        {
            return (long)(DateTime.Now.ToUniversalTime() - unixTime).TotalSeconds;
        }

        /// <summary>
        /// 获取当前时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentMsTimestamp()
        {
            return (long)(DateTime.Now.ToUniversalTime() - unixTime).TotalMilliseconds;
        }
    }
}