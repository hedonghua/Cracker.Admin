using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Cracker.Admin.Repositories;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.Developer
{
    public class GenTableService : ApplicationService, IGenTableService
    {
        private readonly IDatabaseDapperRepository databaseDapperRepository;
        private readonly IRepository<GenTable> genTableRepository;
        private readonly CodeGenerator codeGenerator;
        private readonly IRepository<GenTableColumn> genTableColumnRepository;

        public GenTableService(IDatabaseDapperRepository databaseDapperRepository, IRepository<GenTable> genTableRepository, CodeGenerator codeGenerator
            ,IRepository<GenTableColumn> genTableColumnRepository)
        {
            this.databaseDapperRepository = databaseDapperRepository;
            this.genTableRepository = genTableRepository;
            this.codeGenerator = codeGenerator;
            this.genTableColumnRepository = genTableColumnRepository;
        }

        public async Task AddGenTableAsync(GenTableDto dto)
        {
            if (!await databaseDapperRepository.HasTableAsync(dto.TableName!))
            {
                throw new AbpValidationException($"数据库中没有找到表{dto.TableName}");
            }

            if (await genTableRepository.AnyAsync(x => x.TableName == dto.TableName))
            {
                throw new AbpValidationException("表名已存在");
            }
            var genTable = ObjectMapper.Map<GenTableDto, GenTable>(dto);
            await genTableRepository.InsertAsync(genTable, true);
            await codeGenerator.InitGenTableColumnAsync(dto.TableName!);
        }

        public async Task DeleteGenTableAsync(List<Guid> genTableIds)
        {
            await genTableRepository.DeleteDirectAsync(x => genTableIds.Contains(x.Id));
            await genTableColumnRepository.DeleteDirectAsync(x => genTableIds.Contains(x.GenTableId));
        }

        public async Task<PagedResultStruct<DatabaseTableResultDto>> GetDatabaseTableListAsync(DatabaseableSearchDto dto)
        {
            RefAsync<int> total = new();
            var list = await databaseDapperRepository.GetDatabaseTablesAsync(dto.Page, dto.Size, total, dto.TableName);

            return new PagedResultStruct<DatabaseTableResultDto>(dto)
            {
                TotalCount = total.Value,
                Items = ObjectMapper.Map<List<DatabaseTable>, List<DatabaseTableResultDto>>(list)
            };
        }

        public async Task<PagedResultStruct<GenTableResultDto>> GetGenTableListAsync(GenTableSearchDto dto)
        {
            var query = (await genTableRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(dto.TableName), x => x.TableName.Contains(dto.TableName!))
                .Select(x => new GenTableResultDto
            {
                GenTableId = x.Id,
                TableName = x.TableName,
                Comment = x.Comment,
                BusinessName = x.BusinessName,
                EntityName = x.EntityName,
                ModuleName = x.ModuleName
            });
            return new PagedResultStruct<GenTableResultDto>(dto)
            {
                TotalCount = query.Count(),
                Items = query.StartPage(dto).ToList()
            };
        }

        public async Task<PreviewCodeResultDto> PreviewCodeAsync(Guid genTableId)
        {
            var genTable = await genTableRepository.GetAsync(x => x.Id == genTableId);

            var result = new PreviewCodeResultDto()
            {
                EntityClass = await codeGenerator.BuildEntity(genTable)
            };
            return result;
        }

        public async Task UpdateGenTableAsync(GenTableDto dto)
        {
            var genTable = await genTableRepository.GetAsync(x => x.Id == dto.GenTableId);

            if (!await databaseDapperRepository.HasTableAsync(dto.TableName!))
            {
                throw new AbpValidationException($"数据库中没有找到表{dto.TableName}");
            }

            if (await genTableRepository.AnyAsync(x => x.TableName == dto.TableName) && genTable.TableName != dto.TableName)
            {
                throw new AbpValidationException("表名已存在");
            }

            genTable.TableName = dto.TableName;
            genTable.Comment = dto.Comment;
            genTable.BusinessName = dto.BusinessName;
            genTable.EntityName = dto.EntityName;
            genTable.ModuleName = dto.ModuleName;
            await genTableRepository.UpdateAsync(genTable);
        }
    }
}