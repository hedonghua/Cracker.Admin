using System.Threading.Tasks;

using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Models;

namespace Cracker.Admin.Developer
{
    public interface IGenTableColumnService
    {
        /// <summary>
        /// 查询列配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedResultStruct<GenTableColumnResultDto>> GetGenTableColumnListAsync(GenTableColumnSearchDto dto);

        /// <summary>
        /// 保存列配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task SaveGenTableColumnAsync(GenTableColumnDto dto);
    }
}