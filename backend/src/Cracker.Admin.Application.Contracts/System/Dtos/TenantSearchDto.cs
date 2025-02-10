using Cracker.Admin.Models;

namespace Cracker.Admin.System.Dtos;

public class TenantSearchDto : PageSearch
{
    /// <summary>
    /// 租户名称
    /// </summary>
    public string? Name { get; set; }
}