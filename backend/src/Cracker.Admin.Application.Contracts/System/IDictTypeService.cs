using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;

namespace Cracker.Admin.System
{
    public interface IDictTypeService
    {
        Task AddDictTypeAsync(DictTypeDto dto);

        Task<PagedResultStruct<DictTypeResultDto>> GetDictTypeListAsync(DictTypeSearchDto dto);

        Task UpdateDictTypeAsync(DictTypeDto dto);

        Task DeleteDictTypeAsync(string dictType);

        Task<List<AppOption>> GetDictTypeOptionsAsync(string name);
    }
}