using System.Collections.Generic;

namespace Cracker.Admin.Models
{
    public class AppOptionTree : AppOption
    {
        /// <summary>
        /// 子集
        /// </summary>
        public List<AppOptionTree>? Children { get; set; }
    }
}