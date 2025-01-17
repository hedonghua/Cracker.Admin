using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.System
{
    public interface IDictTypeService
    {
        Task AddDictTypeAsync(DictTypeDto dto);

        Task<PagedResultStruct<DictTypeResultDto>> GetDictTypeListAsync(DictTypeSearchDto dto);

        Task UpdateDictTypeAsync(DictTypeDto dto);

        Task DeleteDictTypeAsync(Guid dictTypeId);
    }
}