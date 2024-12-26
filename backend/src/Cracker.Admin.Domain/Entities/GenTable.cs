using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Volo.Abp.Domain.Entities.Auditing;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 生成表
    /// </summary>
    [Comment("生成表")]
    [Index(nameof(TableName), IsUnique = true)]
    public class GenTable : AuditedEntity<Guid>
    {
        /// <summary>
        /// 表名
        /// </summary>
        [NotNull]
        [Required]
        [Comment("表名")]
        [StringLength(128)]
        public string? TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        [Comment("表描述")]
        [StringLength(256)]
        public string? Comment { get; set; }

        /// <summary>
        /// 业务名
        /// </summary>
        [NotNull]
        [Required]
        [Comment("业务名")]
        [StringLength(64)]
        public string? BusinessName { get; set; }

        /// <summary>
        /// 实体名
        /// </summary>
        [NotNull]
        [Required]
        [Comment("实体名")]
        [StringLength(128)]
        public string? EntityName { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        [NotNull]
        [Required]
        [Comment("模块名")]
        [StringLength(128)]
        public string? ModuleName { get; set; }

        /// <summary>
        /// 实现接口/继承抽象类
        /// </summary>
        [Comment("实现接口/继承抽象类")]
        [StringLength(256)]
        public string? Types { get; set; }

        /// <summary>
        /// 配置列
        /// </summary>
        [ForeignKey("GenTableId")]
        public virtual ICollection<GenTableColumn>? GenTableColumns { get; set; }
    }
}