using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Cracker.Admin.Developer.Dtos;
using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.Developer
{
    public class GenTableColumnService : ApplicationService, IGenTableColumnService
    {
        private readonly IRepository<GenTableColumn> genTableColumnRepository;
        private readonly IRepository<GenTable> genTableRepository;

        public GenTableColumnService(IRepository<GenTableColumn> genTableColumnRepository, IRepository<GenTable> genTableRepository)
        {
            this.genTableColumnRepository = genTableColumnRepository;
            this.genTableRepository = genTableRepository;
        }

        public async Task<PagedResultStruct<GenTableColumnResultDto>> GetGenTableColumnListAsync(GenTableColumnSearchDto dto)
        {
            var query = (await genTableColumnRepository.GetQueryableAsync())
                .Where(x=>x.GenTableId==dto.GenTableId)
                .WhereIf(!string.IsNullOrEmpty(dto.ColumnName), x => x.ColumnName.Contains(dto.ColumnName!));
            var total = query.Count();
            var list = query.StartPage(dto).ToList();
            return new PagedResultStruct<GenTableColumnResultDto>(dto)
            {
                Items = ObjectMapper.Map<List<GenTableColumn>, List<GenTableColumnResultDto>>(list),
                TotalCount = total
            };
        }

        public async Task SaveGenTableColumnAsync(GenTableColumnDto dto)
        {
            var exist = await genTableRepository.AnyAsync(x => x.Id == dto.GenTableId);
            if (!exist) throw new AbpValidationException("生成表配置不存在");

            if(dto.Items == null || dto.Items.Count <= 0) throw new AbpValidationException("生成列配置不能为空");

            var ids = dto.Items.Select(x => x.Id).ToList();
            var columns = await genTableColumnRepository.GetListAsync(x=>ids.Contains(x.Id));
            foreach (var column in columns)
            {
                var item = dto.Items.First(x => x.Id == column.Id);

                column.TableName = item.TableName;
                column.ColumnName = item.ColumnName;
                column.CsharpPropName = item.CsharpPropName;
                column.JsFieldName = item.JsFieldName;
                column.ColumnType = item.ColumnType;
                column.CsharpType = item.CsharpType;
                column.JsType = item.JsType;
                column.HtmlType = item.HtmlType;
                column.Comment = item.Comment;
                column.MaxLength = item.MaxLength;
                column.IsNullable = item.IsNullable;
                column.IsInsert = item.IsInsert;
                column.IsUpdate = item.IsUpdate;
                column.IsSearch = item.IsSearch;
                column.IsShow = item.IsShow;
            }

            await genTableColumnRepository.UpdateManyAsync(columns);
        }
    }
}