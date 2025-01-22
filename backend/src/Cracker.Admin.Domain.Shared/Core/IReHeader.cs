using Microsoft.AspNetCore.Http;

namespace Cracker.Admin.Core
{
    /// <summary>
    /// 本次请求header信息
    /// </summary>
    public interface IReHeader
    {
        /// <summary>
        /// ip
        /// </summary>
        string? Ip { get; }

        /// <summary>
        /// ip实际地址
        /// </summary>
        string? Address { get; }

        /// <summary>
        /// 操作系统
        /// </summary>
        string? Os { get; }

        /// <summary>
        /// 请求地址
        /// </summary>
        string? Url { get; }

        /// <summary>
        /// 浏览器
        /// </summary>
        string? Browser { get; }

        /// <summary>
        /// 操作方法
        /// </summary>
        string? HttpMethod { get; }
    }

    public class ReHeader : IReHeader
    {
        public string? Ip { get; private set; }

        public ReHeader(string? ip, string? address, string? os, string? url, string? httpMethod, string? browser)
        {
            Ip = ip;
            Address = address;
            Os = os;
            Url = url;
            HttpMethod = httpMethod;
            Browser = browser;
        }

        public ReHeader()
        { }

        public static ReHeader Default()
        {
            return new ReHeader();
        }

        public string? Address { get; private set; }

        public string? Os { get; private set; }

        public string? Url { get; private set; }

        public string? Browser { get; private set; }

        public string? HttpMethod { get; private set; }
    }
}