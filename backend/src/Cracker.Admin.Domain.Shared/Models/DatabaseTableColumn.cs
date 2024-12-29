namespace Cracker.Admin.Models
{
    public class DatabaseTableColumn
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [NotNull]
        public string? ColumnName { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        [NotNull]
        public string? ColumnType { get; set; }

        /// <summary>
        /// 列描述
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public long? MaxLength { get; set; }

        /// <summary>
        /// 是否可空 YES/NO
        /// </summary>
        public string? IsNullable { get; set; }
    }
}