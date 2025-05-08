using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cracker.Admin.Models
{
    public class FrontendRoute
    {
        /// <summary>
        /// 菜单路由
        /// </summary>
        [JsonProperty("path")]
        public string? Path { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        [JsonProperty("component")]
        public string? Component { get; set; }

        /// <summary>
        /// 访问权限编码
        /// </summary>
        [JsonProperty("access")]
        public string? Access { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        [JsonProperty("routes")]
        public List<FrontendRoute>? Routes { get; set; }
    }
}