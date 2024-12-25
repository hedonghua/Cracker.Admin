using System;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos
{
    public class DictListDto
    {
        /// <summary>
        /// 字典ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 字典键
        /// </summary>
        [NotNull]
        public string? Key { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [NotNull]
        public string? Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}