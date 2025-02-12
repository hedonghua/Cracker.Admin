namespace Cracker.Admin.Models
{
    public class CreationDbParameter
    {
        public string? Name { get; set; }

        [NotNull]
        [Required]
        public string? ConnectionString { get; set; }
    }
}