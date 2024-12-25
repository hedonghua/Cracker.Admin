using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cracker.Admin.Organization.Dtos
{
    public class PositionGroupDto
    {
        public Guid? Id { get; set; }
        public Guid? ParentId { get; set; }
        public string? GroupName { get; set; }
        public string? Remark { get; set; }
    }
}
