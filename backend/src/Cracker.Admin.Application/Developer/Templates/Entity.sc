using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Cracker.Admin.Domain.Entities.{{moduleName}};

public class {{entityName}}
{
	{{~ for item in columns ~}}
		/// <summary>
        /// {{ item.comment }}
        /// </summary>
        if !item.isNullable
            [NotNull]
            [Required]
        end
        [Comment("{{item.comment}}")]
        if item.columnName!=item.csharpPropName
            [Column("{{item.columnName}}")]
        end
        if item.maxLength>0
          [StringLength({{item.maxLength}})]
        end
        public string? {{item.csharpPropName}} { get; set; }
	{{~ end ~}}
}