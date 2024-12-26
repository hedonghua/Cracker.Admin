using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableDto
    {
        public Guid? GenTableId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        [StringLength(128)]
        public string? TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(256)]
        public string? Comment { get; set; }

        /// <summary>
        /// 业务名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(64)]
        public string? BusinessName { get; set; }

        /// <summary>
        /// 实体名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        public string? EntityName { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        public string? ModuleName { get; set; }
    }
}