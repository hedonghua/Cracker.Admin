using Cracker.Admin.Core;
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
    public class DictService : ApplicationService, IDictService
    {
        private readonly IRepository<SysDict> _dictRepository;
        private readonly IKeySettings _keySettings;

        public DictService(IRepository<SysDict> dictRepository, IKeySettings keySettings)
        {
            _dictRepository = dictRepository;
            _keySettings = keySettings;
        }

        public async Task<bool> AddDictAsync(DictDto dto)
        {
            var isExist = await _dictRepository.AnyAsync(x => x.Key.ToLower() == dto.Key.ToLower());
            if (isExist)
            {
                throw new AbpValidationException("字典键已存在");
            }
            var entity = ObjectMapper.Map<DictDto, SysDict>(dto);
            await _dictRepository.InsertAsync(entity, true);
            if (entity.IsEnabled)
            {
                await _keySettings.SetAsync(entity.Key, entity.Value);
            }
            return true;
        }

        public async Task<bool> DeleteDictAsync(Guid[] ids)
        {
            var entity = await _dictRepository.FindAsync(x => ids.Contains(x.Id))
                ?? throw new AbpValidationException("数据不存在");
            await _dictRepository.DeleteAsync(entity);
            await _keySettings.RemoveAsync(entity.Key);
            return true;
        }

        public async Task<PagedResultDto<DictListDto>> GetDictListAsync(DictQueryDto dto)
        {
            var query = (await _dictRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.Key), x => x.Key.Contains(dto.Key!))
                .WhereIf(!string.IsNullOrEmpty(dto.Label), x => x.Label != null && x.Label.Contains(dto.Label!))
                .WhereIf(!string.IsNullOrEmpty(dto.GroupName), x => x.GroupName != null && x.GroupName.Contains(dto.GroupName!));
            var count = query.Count();
            var rows = query.Skip((dto.Page - 1) * dto.Size).Take(dto.Size).ToList();
            return new PagedResultDto<DictListDto>(count, ObjectMapper.Map<List<SysDict>, List<DictListDto>>(rows));
        }

        public async Task<bool> RefreshCacheAsync()
        {
            await _keySettings.InitializationAsync(true);
            return true;
        }

        public async Task<bool> UpdateDictAsync(DictDto dto)
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
            entity.GroupName = dto.GroupName;
            entity.Label = dto.Label;
            entity.Sort = dto.Sort;
            entity.Remark = dto.Remark;
            entity.IsEnabled = dto.IsEnabled;
            await _dictRepository.UpdateAsync(entity, true);
            if (entity.IsEnabled)
            {
                await _keySettings.SetAsync(entity.Key, entity.Value);
            }
            else
            {
                await _keySettings.RemoveAsync(entity.Key);
            }
            return true;
        }
    }
}