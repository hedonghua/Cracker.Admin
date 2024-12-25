using System;
using System.Collections.Generic;

namespace Cracker.Admin.Models
{
    public class FrontRoute
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 菜单路由
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// meta
        /// </summary>
        public FrontRouteMeta? Meta { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<FrontRoute>? Children { get; set; }
    }

    public class FrontRouteMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }
    }
}