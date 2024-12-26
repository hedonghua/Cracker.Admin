using System;
using System.Threading.Tasks;

using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Models;

using Volo.Abp.Application.Services;

namespace Cracker.Admin.Developer
{
    public class GenTableColumnService : ApplicationService, IGenTableColumnService
    {
        public Task<PagedResultStruct<GenTableColumnResultDto>> GetGenTableColumnListAsync(GenTableColumnSearchDto dto)
        {
            throw new NotImplementedException();
        }

        public Task SaveGenTableColumnAsync(GenTableColumnDto dto)
        {
            throw new NotImplementedException();
        }
    }
}