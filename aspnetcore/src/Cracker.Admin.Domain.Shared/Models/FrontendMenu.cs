using System.Collections.Generic;

namespace Cracker.Admin.Models
{
    public class FrontendMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 组件名
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool HideInMenu { get; set; }

        /// <summary>
        /// 菜单路由
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<FrontendMenu>? Children { get; set; }
    }
}