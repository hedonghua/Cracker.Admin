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

        /// <summary>
        /// 小写加下划线命名转帕斯卡命名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // 使用StringBuilder来构建结果字符串
            StringBuilder pascalCase = new StringBuilder();

            // 分割字符串
            string[] words = input.Split('_');

            // 遍历每个单词，并将其首字母大写
            foreach (string word in words)
            {
                if (word.Length > 0)
                {
                    pascalCase.Append(char.ToUpperInvariant(word[0]));
                    if (word.Length > 1)
                    {
                        pascalCase.Append(word.Substring(1).ToLowerInvariant());
                    }
                }
            }

            return pascalCase.ToString();
        }
    }
}