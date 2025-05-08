using Microsoft.EntityFrameworkCore;

namespace Cracker.Admin.Entities
{
    public class Notification : AuditedEntity<Guid>
    {
        /// <summary>
        /// 通知标题
        /// </summary>
        [NotNull]
        [Required]
        [MaxLength(100)]
        [Comment("通知标题")]
        public string? Title { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        [MaxLength(500)]
        [Comment("通知描述")]
        public string? Description { get; set; }

        /// <summary>
        /// 通知员工
        /// </summary>
        [NotNull]
        [Required]
        [Comment("通知员工")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 是否已读(1已读0未读)
        /// </summary>
        [Required]
        [Comment("是否已读(1已读0未读)")]
        public bool IsReaded { get; set; }

        /// <summary>
        /// 已读时间
        /// </summary>
        [Required]
        [Comment("已读时间")]
        public DateTime? ReadedTime { get; set; }
    }
}