using System;

namespace Cracker.Admin.System.LogManagement.Dtos
{
    public class BusinessLogListDto
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 操作方法，全名
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// http请求方式
        /// </summary>
        public string? HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 操作节点名
        /// </summary>
        public string? NodeName { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 耗时，单位毫秒
        /// </summary>
        public int MillSeconds { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 系统
        /// </summary>
        public string? Os { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string? Browser { get; set; }
    }
}