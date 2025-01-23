using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 租户表
    /// </summary>
    [Table("sys_tenant")]
    [Comment("租户表")]
    public class SysTenant : AuditedEntity<string>
    {
        public SysTenant(string id) 
        {
            Id = id;
        }

        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [StringLength(128)]
        [Comment("租户名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        [Required]
        [StringLength(512)]
        [Comment("连接字符串")]
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Redis连接
        /// </summary>
        [Required]
        [StringLength(512)]
        [Comment("Redis连接")]
        public string? RedisConnection { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        [Comment("备注")]
        public string? Remark { get; set; }
    }
}