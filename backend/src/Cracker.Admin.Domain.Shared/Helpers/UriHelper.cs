using System.Collections.Generic;

namespace Cracker.Admin.Helpers
{
    public class UriHelper
    {
        /// <summary>
        /// url拼接，处理拼接多余斜杆'/'，
        /// 处理重复拼接根地址
        /// </summary>
        /// <param name="allowRepeat">是否允许重复</param>
        /// <param name="urls"></param>
        /// <returns></returns>
        public static string Concat(bool allowRepeat = true, params string?[] urls)
        {
            if (urls == null || urls.Length == 0) return string.Empty;
            var strs = new List<string>();
            var baseAddress = string.Empty;
            for (int i = 0; i < urls.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(urls[i])) continue;
                var temp = urls[i]!.Trim('/').ToLower();
                if (!allowRepeat && strs.Contains(temp)) continue;
                //获取第一个带http,https的为根地址
                if (string.IsNullOrWhiteSpace(baseAddress) && temp.Contains("http") || temp.Contains("https"))
                {
                    baseAddress = temp;
                }
                else if (temp.Contains(baseAddress) && !string.IsNullOrEmpty(baseAddress))
                {
                    temp = temp.Replace(baseAddress, "").TrimStart('/');
                }
                strs.Add(temp);
            }
            return string.Join('/', strs);
        }
    }
}