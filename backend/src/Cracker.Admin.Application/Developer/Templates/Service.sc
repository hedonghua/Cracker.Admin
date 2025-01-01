using Cracker.Admin.Application.Contracts.{{moduleName}};
using Cracker.Admin.Application.Contracts.{{moduleName}}.Dtos;
using Cracker.Admin.Domain.Entities.{{moduleName}};
using Cracker.Admin.Domain.Shared.Extensions;
using Cracker.Admin.Domain.Shared.Models;

using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Cracker.Admin.Application.{{moduleName}};

public class {{businessName}}Service : ApplicationService, I{{businessName}}Service
{
    private readonly IRepository<{{entityName}}> {{businessNameOfFirstLower}}Repository;

    public {{businessName}}Service(IRepository<{{entityName}}> {{businessNameOfFirstLower}}Repository)
    {
        this.{{businessNameOfFirstLower}}Repository = {{businessNameOfFirstLower}}Repository;
    }

    public async Task Add{{businessName}}Async({{businessName}}Dto dto)
    {
        var entity = new {{entityName}}();
        {{addFields}}
        await {{businessNameOfFirstLower}}Repository.InsertAsync(entity);
    }

    public async Task Delete{{businessName}}Async(Guid {{businessNameOfFirstLower}}Id)
    {
        await {{businessNameOfFirstLower}}Repository.DeleteAsync(x => x.Id == {{businessName}}Id);
    }

    public async Task<PagedResultStruct<{{businessName}}ResultDto>> Get{{businessName}}ListAsync({{businessName}}SearchDto dto)
    {
        var query = (await {{businessNameOfFirstLower}}Repository.GetQueryableAsync()){{queryConditions}}
            .Select(x => new {{businessName}}ResultDto
            {
                {{queryMapper}}
            });
        return new PagedResultStruct<{{businessName}}ResultDto>(dto)
        {
            Total = query.Count(),
            Rows = query.StartPage(dto).ToList()
        };
    }

    public async Task Update{{businessName}}Async({{businessName}}Dto dto)
    {
        var entity = await {{businessNameOfFirstLower}}Repository.GetAsync(x => x.Id == dto.{{businessName}}Id);
        {{updateFields}}
        await {{businessNameOfFirstLower}}Repository.UpdateAsync({{businessName}});
    }
}