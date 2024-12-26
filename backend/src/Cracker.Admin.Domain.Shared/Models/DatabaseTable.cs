namespace Cracker.Admin.Models
{
    public class DatabaseTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 表行数
        /// </summary>
        public long Rows { get; set; }
    }
}