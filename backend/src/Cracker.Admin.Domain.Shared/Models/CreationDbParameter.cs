namespace Cracker.Admin.Models
{
    public class CreationDbParameter
    {
        [NotNull]
        [Required]
        public string? ConnectionString { get; set; }
    }
}