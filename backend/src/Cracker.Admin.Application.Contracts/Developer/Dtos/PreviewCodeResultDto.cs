using System.Collections.Generic;

using Cracker.Admin.Models;

namespace Cracker.Admin.Developer.Dtos
{
    public class PreviewCodeResultDto
    {
        public AppOption? EntityClass { get; set; }
        public AppOption? IService { get; set; }
        public AppOption? Service { get; set; }
        public AppOption? EntityDto { get; set; }
        public AppOption? EntitySearchDto { get; set; }
        public AppOption? EntityResultDto { get; set; }
        public AppOption? Controller { get; set; }
    }
}