using Cracker.Admin.Entity.Enums;

namespace Cracker.Admin.Entity
{
    /// <summary>
    /// 文件表
    /// </summary>
    public class SysFile : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 文件编号/BLOB名，唯一
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(128)]
        public string? UniqueName { get; set; }

        /// <summary>
        /// 文件名，唯一
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(256)]
        public string? FileName { get; set; }

        /// <summary>
        /// 单位KB
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string? Suffix { get; set; }

        /// <summary>
        /// 相对地址
        /// </summary>
        [StringLength(512)]
        public string? RelativeUrl { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        [StringLength(64)]
        public string? MimeType { get; set; }

        /// <summary>
        /// 额外地址
        /// </summary>
        [StringLength(512)]
        public string? ExtraPath { get; set; }

        /// <summary>
        /// 上传类型 9BLOB文件系统
        /// </summary>
        public UploadType UploadType { get; set; } = 0;
    }
}