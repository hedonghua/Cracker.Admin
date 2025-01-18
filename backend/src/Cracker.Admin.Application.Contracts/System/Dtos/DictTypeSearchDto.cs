using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Cracker.Admin.Models;

namespace Cracker.Admin.System.Dtos;

public class DictTypeSearchDto : PageSearch
{
    public string? Name { get; set; }

    public string? DictType { get; set; }
}