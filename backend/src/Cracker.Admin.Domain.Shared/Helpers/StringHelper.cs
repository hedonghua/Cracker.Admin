using System.Text;

namespace Cracker.Admin.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <param name="len">字符长度</param>
        /// <param name="isNumber">是否纯数字</param>
        /// <param name="isWord">是否纯字母</param>
        /// <param name="hasChar">是否含特殊符号</param>
        /// <returns></returns>
        public static string RandomStr(int len, bool isNumber = false, bool isWord = false, bool hasChar = false)
        {
            var sb = new StringBuilder();
            if (isNumber)
            {
                Random r = new Random();
                for (int i = 0; i < len; i++)
                {
                    sb.Append(r.Next(0, 10));
                }
            }
            return sb.ToString();
        }
    }
}