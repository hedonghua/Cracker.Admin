using System;

using Cracker.Admin.Enums;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableColumnResultDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 生成表ID
        /// </summary>
        public Guid GenTableId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string? ColumnName { get; set; }

        /// <summary>
        /// C#属性名
        /// </summary>
        public string? CsharpPropName { get; set; }

        /// <summary>
        /// JS字段名
        /// </summary>
        public string? JsFieldName { get; set; }

        /// <summary>
        /// 数据库列类型
        /// </summary>
        public string? ColumnType { get; set; }

        /// <summary>
        /// C#类型
        /// </summary>
        public string? CsharpType { get; set; }

        /// <summary>
        /// JS类型
        /// </summary>
        public string? JsType { get; set; }

        /// <summary>
        /// HTML类型
        /// </summary>
        public string? HtmlType { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public long? MaxLength { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 是否参与新增
        /// </summary>
        public bool IsInsert { get; set; }

        /// <summary>
        /// 是否参与修改
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 是否参与搜索
        /// </summary>
        public bool IsSearch { get; set; }

        /// <summary>
        /// 搜索类型
        /// </summary>
        public SearchType SearchType { get; set; }

        /// <summary>
        /// 是否在表格中显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}