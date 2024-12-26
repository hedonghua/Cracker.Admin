using Cracker.Admin.Entities.Enums;

namespace Cracker.Admin.Entities
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class SysMenu : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 显示标题/名称
        /// </summary>
        [NotNull]
        [Required]
        [StringLength(32)]
        public string? Title { get; set; }

        /// <summary>
        /// 组件名
        /// </summary>
        [StringLength(32)]
        public string? Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(64)]
        public string? Icon { get; set; }

        /// <summary>
        /// 路由/地址
        /// </summary>
        [StringLength(256)]
        public string? Path { get; set; }

        /// <summary>
        /// 功能类型
        /// </summary>
        public FunctionType FunctionType { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        [StringLength(128)]
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
        public bool Hidden { get; set; }
    }
}