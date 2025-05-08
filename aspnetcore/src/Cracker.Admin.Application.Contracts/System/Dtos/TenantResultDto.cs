using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos;

public class TenantResultDto
{
    [NotNull]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 租户名称
    /// </summary>
    [NotNull]
    [Required]
    public string? Name { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    [NotNull]
    [Required]
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Redis连接
    /// </summary>
    [NotNull]
    [Required]
    public string? RedisConnection { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}