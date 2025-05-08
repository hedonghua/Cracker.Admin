using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos
{
    public class NotificationDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        [NotNull]
        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 通知员工
        /// </summary>
        [NotNull]
        [Required]
        public Guid EmployeeId { get; set; }
    }
}