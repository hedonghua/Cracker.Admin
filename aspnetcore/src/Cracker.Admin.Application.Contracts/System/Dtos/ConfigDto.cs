using System.ComponentModel.DataAnnotations;

namespace Cracker.Admin.System.Dtos
{
    public class ConfigDto
    {
        [Required]
        public string? Key { get; set; }

        [Required]
        public string? Value { get; set; }
    }
}