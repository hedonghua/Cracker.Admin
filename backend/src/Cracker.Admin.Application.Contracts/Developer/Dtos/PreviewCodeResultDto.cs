using System.Collections.Generic;

using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class PreviewCodeResultDto
    {
        public AppOption? EntityClass { get; set; }
        public AppOption? IService { get; set; }
        public AppOption? Service { get; set; }
    }
}