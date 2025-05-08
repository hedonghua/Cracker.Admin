using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System;

public class DictTypeService : ApplicationService, IDictTypeService
{
    private readonly IRepository<SysDictType> dictTypeRepository;
    private readonly IRepository<SysDictData> dictDataRepository;

    public DictTypeService(IRepository<SysDictType> dictTypeRepository, IRepository<SysDictData> dictDataRepository)
    {
        this.dictTypeRepository = dictTypeRepository;
        this.dictDataRepository = dictDataRepository;
    }

    public async Task AddDictTypeAsync(DictTypeDto dto)
    {
        if (await dictTypeRepository.AnyAsync(x => x.DictType.ToLower() == dto.DictType.ToLower()))
        {
            throw new BusinessException(message: "字典类型已存在");
        }

        var entity = new SysDictType
        {
            Name = dto.Name,
            IsEnabled = dto.IsEnabled,
            DictType = dto.DictType,
            Remark = dto.Remark
        };

        await dictTypeRepository.InsertAsync(entity);
    }

    public async Task DeleteDictTypeAsync(string dictType)
    {
        await dictTypeRepository.DeleteDirectAsync(x => x.DictType == dictType);
    }

    public async Task<PagedResultStruct<DictTypeResultDto>> GetDictTypeListAsync(DictTypeSearchDto dto)
    {
        var query = (await dictTypeRepository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrEmpty(dto.Name), x => x.Name.Contains(dto.Name!))
            .WhereIf(!string.IsNullOrEmpty(dto.DictType), x => x.DictType.Contains(dto.DictType!))
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
        if (!entity.DictType.Equals(dto.DictType, StringComparison.CurrentCultureIgnoreCase) && await dictTypeRepository.AnyAsync(x => x.DictType.ToLower() == dto.DictType.ToLower()))
        {
            throw new BusinessException(message: "字典类型已存在");
        }

        entity.Name = dto.Name;
        entity.IsEnabled = dto.IsEnabled;
        entity.DictType = dto.DictType;
        entity.Remark = dto.Remark;

        await dictTypeRepository.UpdateAsync(entity);
    }

    public async Task<List<AppOption>> GetDictDataOptionsAsync(string type)
    {
        return (await dictDataRepository.GetQueryableAsync())
            .Where(x => x.DictType == type)
            .OrderBy(x => x.Sort)
            .Select(x => new AppOption(x.Label, x.Value)).ToList();
    }
}