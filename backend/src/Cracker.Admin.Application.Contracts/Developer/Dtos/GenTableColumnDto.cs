using System;
using System.Collections.Generic;

namespace Cracker.Admin.Developer.Dtos
{
    public class GenTableColumnDto
    {
        public Guid GenTableId { get; set; }
        public List<GenTableColumnItemDto>? Items { get; set; }
    }

    public class GenTableColumnItemDto
    {

    }
}