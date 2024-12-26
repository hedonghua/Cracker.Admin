using Cracker.Admin.Application.Contracts.{{moduleName}};
using Cracker.Admin.Application.Contracts.{{moduleName}}.Dtos;
using Cracker.Admin.Domain.Entities;
using Cracker.Admin.Domain.Shared.Extensions;
using Cracker.Admin.Domain.Shared.Models;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.Application.{{moduleName}}
{
    public class {{businessName}}Service : ApplicationService, I{{businessName}}Service
    {
        private readonly IRepository<{{entityName}}> {{businessName}}Repository;

        public {{businessName}}Service(IRepository<{{entityName}}> {{businessName}}Repository)
        {
            this.{{businessName}}Repository = {{businessName}}Repository;
        }

        public async Task Add{{businessName}}Async({{businessName}}Dto dto)
        {
            var entity = ObjectMapper.Map<{{businessName}}Dto, {{entityName}}>(dto);
            await {{businessName}}Repository.InsertAsync(entity);
        }

        public async Task Delete{{businessName}}Async(Guid {{businessName}}Id)
        {
            await {{businessName}}Repository.DeleteAsync(x => x.Id == {{businessName}}Id);
        }

        public async Task<PagedResult<{{businessName}}ResultDto>> Get{{businessName}}ListAsync({{businessName}}SearchDto dto)
        {
            var query = (await {{businessName}}Repository.GetQueryableAsync()).Select(x => new {{businessName}}ResultDto
            {
                {{businessName}}Id = x.Id,
                {{businessName}}Name = x.{{businessName}}Name,
                Remark = x.Remark,
            });
            return new PagedResult<{{businessName}}ResultDto>(dto)
            {
                Total = query.Count(),
                Rows = query.StartPage(dto).ToList()
            };
        }

        public async Task Update{{businessName}}Async({{businessName}}Dto dto)
        {
            var entity = await {{businessName}}Repository.GetAsync(x => x.Id == dto.{{businessName}}Id);

            entity.{{businessName}}Name = dto.{{businessName}}Name;
            {{businessName}}.Remark = dto.Remark;

            await {{businessName}}Repository.UpdateAsync({{businessName}});
        }
    }
}