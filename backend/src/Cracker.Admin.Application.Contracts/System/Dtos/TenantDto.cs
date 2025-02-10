using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos
{
    public class TenantDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Required]
        [StringLength(512)]
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Redis连接
        /// </summary>
        [Required]
        [StringLength(512)]
        public string? RedisConnection { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string? Remark { get; set; }
    }
}