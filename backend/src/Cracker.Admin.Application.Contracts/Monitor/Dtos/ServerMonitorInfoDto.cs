using System.Collections.Generic;

namespace Cracker.Admin.Monitor.Dtos
{
    public class ServerMonitorInfoDto
    {
        /// <summary>
        /// CPU信息
        /// </summary>
        public Dictionary<string, string>? Cpu { get; set; }

        /// <summary>
        /// 内存信息
        /// </summary>
        public Dictionary<string, string>? Memory { get; set; }

        /// <summary>
        /// 服务器信息
        /// </summary>
        public Dictionary<string, string>? Server { get; set; }

        /// <summary>
        /// 磁盘信息
        /// </summary>
        public Dictionary<string, string>? Disk { get; set; }

        /// <summary>
        /// .NET信息
        /// </summary>
        public Dictionary<string, string>? Dotnet { get; set; }
    }
}