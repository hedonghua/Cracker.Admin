using System;

namespace Cracker.Admin.Oss
{
    public class FileManager
    {
        /// <summary>
        /// 预览API地址
        /// </summary>
        public const string PREVIEW_API = "oss/preview/{id:Guid}";

        /// <summary>
        /// 获取实际预览相对地址
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        public static string GetRealPreviewApiUrl(Guid blob)
        {
            return PREVIEW_API.Replace("{id:Guid}", blob.ToString());
        }
    }
}