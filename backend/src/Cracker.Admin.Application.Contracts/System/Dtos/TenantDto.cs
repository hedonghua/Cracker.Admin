using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class TenantDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
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
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string? Remark { get; set; }
    }
}