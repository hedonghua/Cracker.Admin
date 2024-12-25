using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.Oss.Dtos
{
    public class UploadDto
    {
        /// <summary>
        /// 文件字节
        /// </summary>
        [NotNull]
        [Required]
        public byte[]? Bytes { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>
        [NotNull]
        [Required]
        public string? FileName { get; set; }

        /// <summary>
        /// 文件大小，单位B
        /// </summary>
        public int Size { get; set; }
    }
}