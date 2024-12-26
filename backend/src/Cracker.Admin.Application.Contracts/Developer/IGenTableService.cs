using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Models;

namespace Cracker.Admin.Developer
{
    public interface IGenTableService
    {
        Task<PagedResultStruct<DatabaseTableResultDto>> GetDatabaseTableListAsync(DatabaseableSearchDto dto);

        Task<PagedResultStruct<GenTableResultDto>> GetGenTableListAsync(GenTableSearchDto dto);

        Task AddGenTableAsync(GenTableDto dto);

        Task UpdateGenTableAsync(GenTableDto dto);

        Task DeleteGenTableAsync(List<Guid> genTableIds);

        /// <summary>
        /// 预览代码
        /// </summary>
        /// <param name="genTableId"></param>
        /// <returns></returns>
        Task<PreviewCodeResultDto> PreviewCodeAsync(Guid genTableId);
    }
}