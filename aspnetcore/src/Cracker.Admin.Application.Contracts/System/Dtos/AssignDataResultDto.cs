using System;

namespace Cracker.Admin.System.Dtos
{
    public class AssignDataResultDto
    {
        public int PowerDataType { get; set; }

        public Guid[]? DeptIds { get; set; }
    }
}