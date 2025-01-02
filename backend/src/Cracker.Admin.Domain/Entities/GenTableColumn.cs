using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using Cracker.Admin.Enums;

using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 生成表的配置列
    /// </summary>
    [Table("gen_table_column")]
    [Comment("生成表的配置列")]
    public class GenTableColumn : AuditedEntity<Guid>
    {
        /// <summary>
        /// 生成表ID
        /// </summary>
        [Required]
        public Guid GenTableId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        [NotNull]
        [Comment("表名")]
        [StringLength(128)]
        public string? TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Required]
        [NotNull]
        [Comment("列名")]
        [StringLength(128)]
        public string? ColumnName { get; set; }

        /// <summary>
        /// C#属性名
        /// </summary>
        [Required]
        [NotNull]
        [Comment("C#属性名")]
        [StringLength(128)]
        public string? CsharpPropName { get; set; }

        /// <summary>
        /// JS字段名
        /// </summary>
        [Required]
        [NotNull]
        [Comment("JS字段名")]
        [StringLength(128)]
        public string? JsFieldName { get; set; }

        /// <summary>
        /// 数据库列类型
        /// </summary>
        [Required]
        [NotNull]
        [Comment("数据库列类型")]
        [StringLength(32)]
        public string? ColumnType { get; set; }

        /// <summary>
        /// C#类型
        /// </summary>
        [Comment("C#类型")]
        [StringLength(32)]
        public string? CsharpType { get; set; }

        /// <summary>
        /// JS类型
        /// </summary>
        [Comment("JS类型")]
        [StringLength(32)]
        public string? JsType { get; set; }

        /// <summary>
        /// HTML类型
        /// </summary>
        [Comment("HTML类型")]
        [StringLength(32)]
        public string? HtmlType { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        [Comment("列描述")]
        [StringLength(256)]
        public string? Comment { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        [Comment("最大长度")]
        public long? MaxLength { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        [Required]
        [Comment("是否可空")]
        public bool IsNullable { get; set; }

        /// <summary>
        /// 是否参与新增
        /// </summary>
        [Required]
        [Comment("是否参与新增")]
        public bool IsInsert { get; set; }

        /// <summary>
        /// 是否参与修改
        /// </summary>
        [Required]
        [Comment("是否参与修改")]
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 是否参与搜索
        /// </summary>
        [Required]
        [Comment("是否参与搜索")]
        [DefaultValue(false)]
        public bool IsSearch { get; set; }

        /// <summary>
        /// 搜索类型
        /// </summary>
        [Required]
        [Comment("搜索类型")]
        [DefaultValue(0)]
        public SearchType SearchType { get; set; }

        /// <summary>
        /// 是否在表格中显示
        /// </summary>
        [Required]
        [Comment("是否在表格中显示")]
        [DefaultValue(true)]
        public bool IsShow { get; set; }
    }
}