using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.Application.System;

public class DictTypeService : ApplicationService, IDictTypeService
{
    private readonly IRepository<SysDictType> dictTypeRepository;

    public DictTypeService(IRepository<SysDictType> dictTypeRepository)
    {
        this.dictTypeRepository = dictTypeRepository;
    }

    public async Task AddDictTypeAsync(DictTypeDto dto)
    {
        var entity = new SysDictType();
        entity.Name = dto.Name;
        entity.IsEnabled = dto.IsEnabled;
        entity.DictType = dto.DictType;
        entity.Remark = dto.Remark;

        await dictTypeRepository.InsertAsync(entity);
    }

    public async Task DeleteDictTypeAsync(Guid dictTypeId)
    {
        await dictTypeRepository.DeleteAsync(x => x.Id == dictTypeId);
    }

    public async Task<PagedResultStruct<DictTypeResultDto>> GetDictTypeListAsync(DictTypeSearchDto dto)
    {
        var query = (await dictTypeRepository.GetQueryableAsync())
            .Select(x => new DictTypeResultDto
            {
                Name = x.Name,
                Id = x.Id,
                IsEnabled = x.IsEnabled,
                DictType = x.DictType,
                Remark = x.Remark
            });
        return new PagedResultStruct<DictTypeResultDto>(dto)
        {
            TotalCount = query.Count(),
            Items = query.StartPage(dto).ToList()
        };
    }

    public async Task UpdateDictTypeAsync(DictTypeDto dto)
    {
        var entity = await dictTypeRepository.GetAsync(x => x.Id == dto.Id);
        entity.Name = dto.Name;
        entity.IsEnabled = dto.IsEnabled;
        entity.DictType = dto.DictType;
        entity.Remark = dto.Remark;

        await dictTypeRepository.UpdateAsync(entity);
    }
}