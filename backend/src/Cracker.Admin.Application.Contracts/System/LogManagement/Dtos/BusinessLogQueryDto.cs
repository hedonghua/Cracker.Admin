using Cracker.Admin.Models;

namespace Cracker.Admin.System.LogManagement.Dtos
{
    public class BusinessLogQueryDto : PageSearch
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? UserName { get; set; }

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
        public int Status { get; set; }

        /// <summary>
        /// 耗时，单位毫秒
        /// </summary>
        public int MillSeconds { get; set; }
    }
}