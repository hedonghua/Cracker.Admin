using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Cracker.Admin.Domain.Entities.{{ moduleName }};

/// <summary>
/// {{ comment }}
/// </summary>
[Table("{{ tableName }}")]
[Comment("{{ comment }}")]
public class {{ entityName }} {{ baseClass }}
{
{{ properties }}
}