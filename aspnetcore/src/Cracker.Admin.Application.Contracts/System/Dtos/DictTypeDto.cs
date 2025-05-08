using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cracker.Admin.System.Dtos;

public class DictTypeDto
{
    public string? Name { get; set; }

    public Guid? Id { get; set; }

    [NotNull]
    [Required]
    public bool IsEnabled { get; set; }

    [NotNull]
    [Required]
    public string? DictType { get; set; }

    public string? Remark { get; set; }
}