using System;

namespace Cracker.Admin.Core
{
    /// <summary>
    /// 文件日志
    /// </summary>
    public interface IFileLogger
    {
        /// <summary>
        /// 按每天写入
        /// </summary>
        /// <param name="content">内容，支持string.Format</param>
        /// <param name="topic">主题，作为文件名，不传则使用调用方文件名</param>
        /// <param name="exception">异常</param>
        /// <param name="args">参数</param>
        void Write(string content, string? topic = default, Exception? exception = default, params object[] args);
    }
}