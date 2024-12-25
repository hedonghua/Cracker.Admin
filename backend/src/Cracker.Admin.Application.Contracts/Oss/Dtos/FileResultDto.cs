using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.Oss.Dtos
{
    public class FileResultDto
    {
        /// <summary>
        /// 文件字节
        /// </summary>
        [NotNull]
        public byte[]? Bytes { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [NotNull]
        public string? FileName { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        [NotNull]
        public string? MimeType { get; set; }
    }
}