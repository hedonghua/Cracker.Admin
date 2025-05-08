using System;
using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.Organization.Dtos
{
    public class EmployeeBindUserDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
    }
}