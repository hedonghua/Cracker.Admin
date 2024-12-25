using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.LogManagement.Dtos
{
    public class BusinessLogDto
    {
        /// <summary>
        /// 操作方法，全名
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// 操作节点名
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        [StringLength(128)]
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 耗时，单位毫秒
        /// </summary>
        public int MillSeconds { get; set; }
    }
}