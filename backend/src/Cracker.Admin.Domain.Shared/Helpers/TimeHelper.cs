namespace Cracker.Admin.Helpers
{
    public class TimeHelper
    {
        private static readonly DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimestamp()
        {
            return (long)(DateTime.Now.ToUniversalTime() - unixTime).TotalSeconds;
        }
    }
}