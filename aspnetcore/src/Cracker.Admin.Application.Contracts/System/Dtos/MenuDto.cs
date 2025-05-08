using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos
{
    public class MenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 显示标题/名称
        /// </summary>
        [NotNull]
        [Required]
        public string? Title { get; set; }

        /// <summary>
        /// 组件名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 路由/地址
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        public int FunctionType { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        [NotNull]
        [Required]
        public string? Permission { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Required]
        public bool Hidden { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string? Component { get; set; }
    }
}