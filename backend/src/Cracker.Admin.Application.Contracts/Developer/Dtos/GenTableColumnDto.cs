using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using Cracker.Admin.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableColumnDto
    {
        [Required]
        public Guid GenTableId { get; set; }

        public List<GenTableColumnItemDto>? Items { get; set; }
    }

    public class GenTableColumnItemDto
    {
        public Guid Id { get; set; }

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
        [StringLength(128)]
        public string? TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(128)]
        public string? ColumnName { get; set; }

        /// <summary>
        /// C#属性名
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(128)]
        public string? CsharpPropName { get; set; }

        /// <summary>
        /// JS字段名
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(128)]
        public string? JsFieldName { get; set; }

        /// <summary>
        /// 数据库列类型
        /// </summary>
        [Required]
        [NotNull]
        [StringLength(32)]
        public string? ColumnType { get; set; }

        /// <summary>
        /// C#类型
        /// </summary>
        [StringLength(32)]
        public string? CsharpType { get; set; }

        /// <summary>
        /// JS类型
        /// </summary>
        [StringLength(32)]
        public string? JsType { get; set; }

        /// <summary>
        /// HTML类型
        /// </summary>
        [StringLength(32)]
        public string? HtmlType { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        [StringLength(256)]
        public string? Comment { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public long? MaxLength { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        [Required]
        public bool IsNullable { get; set; }

        /// <summary>
        /// 是否参与新增
        /// </summary>
        [Required]
        public bool IsInsert { get; set; }

        /// <summary>
        /// 是否参与修改
        /// </summary>
        [Required]
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 是否参与搜索
        /// </summary>
        [Required]
        public bool IsSearch { get; set; }

        /// <summary>
        /// 搜索类型
        /// </summary>
        [Required]
        public SearchType SearchType { get; set; }

        /// <summary>
        /// 是否在表格中显示
        /// </summary>
        [Required]
        public bool IsShow { get; set; }
    }
}