using Cracker.Admin.Entities;
using Cracker.Admin.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.System
{
    public class DictDataService : ApplicationService, IDictDataService
    {
        private readonly IRepository<SysDictData> _dictRepository;

        public DictDataService(IRepository<SysDictData> dictRepository)
        {
            _dictRepository = dictRepository;
        }

        public async Task<bool> AddDictDataAsync(DictDataDto dto)
        {
            var isExist = await _dictRepository.AnyAsync(x => x.Key.ToLower() == dto.Key.ToLower());
            if (isExist)
            {
                throw new AbpValidationException("字典键已存在");
            }
            var entity = ObjectMapper.Map<DictDataDto, SysDictData>(dto);
            await _dictRepository.InsertAsync(entity, true);
            return true;
        }

        public async Task<bool> DeleteDictDataAsync(Guid[] ids)
        {
            var entity = await _dictRepository.FindAsync(x => ids.Contains(x.Id))
                ?? throw new AbpValidationException("数据不存在");
            await _dictRepository.DeleteAsync(entity);
            return true;
        }

        public async Task<PagedResultDto<DictDataListDto>> GetDictDataListAsync(DictDataQueryDto dto)
        {
            var query = (await _dictRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.Key), x => x.Key.Contains(dto.Key!))
                .WhereIf(!string.IsNullOrEmpty(dto.Label), x => x.Label != null && x.Label.Contains(dto.Label!))
                .WhereIf(!string.IsNullOrEmpty(dto.DictType), x => x.DictType != null && x.DictType.Contains(dto.DictType!));
            var count = query.Count();
            var rows = query.OrderBy(x => x.Sort).ThenByDescending(x => x.CreationTime).Skip((dto.Current - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            return new PagedResultDto<DictDataListDto>(count, ObjectMapper.Map<List<SysDictData>, List<DictDataListDto>>(rows));
        }

        public async Task<bool> UpdateDictDataAsync(DictDataDto dto)
        {
            if (!dto.Id.HasValue) throw new ArgumentNullException(nameof(dto.Id));
            var entity = await _dictRepository.FindAsync(x => x.Id == dto.Id)
                ?? throw new AbpValidationException("数据不存在");
            var isExist = await _dictRepository.AnyAsync(x => x.Key.ToLower() == dto.Key.ToLower());
            if (entity.Key.ToLower() != dto.Key.ToLower() && isExist)
            {
                throw new AbpValidationException("字典键已存在");
            }
            entity.Key = dto.Key;
            entity.Value = dto.Value;
            entity.DictType = dto.DictType;
            entity.Label = dto.Label;
            entity.Sort = dto.Sort;
            entity.Remark = dto.Remark;
            entity.IsEnabled = dto.IsEnabled;
            await _dictRepository.UpdateAsync(entity, true);
            return true;
        }
    }
}