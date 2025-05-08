using Cracker.Admin.Models;
using System.Collections.Generic;

namespace Cracker.Admin.System.Dtos
{
    public class MenuOptionTreeDto : AppOptionTree
    {
        public string? Key { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public new List<MenuOptionTreeDto>? Children { get; set; }
    }
}