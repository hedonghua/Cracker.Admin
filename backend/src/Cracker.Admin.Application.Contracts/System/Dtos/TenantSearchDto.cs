using Cracker.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos;

public class TenantSearchDto : PageSearch
{
    /// <summary>
    /// 租户名称
    /// </summary>
    [NotNull]
    [Required]
    public string? Name { get; set; }
}