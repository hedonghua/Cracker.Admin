using Cracker.Admin.Application.Contracts.{{moduleName}}.Dtos;
using Cracker.Admin.Domain.Shared.Models;

namespace Cracker.Admin.Application.Contracts.{{moduleName}};

public interface I{{businessName}}Service
{
    Task Add{{businessName}}Async({{businessName}}Dto dto);

    Task<PagedResult<{{businessName}}ResultDto>> Get{{businessName}}ListAsync({{businessName}}SearchDto dto);

    Task Update{{businessName}}Async({{businessName}}Dto dto);

    Task Delete{{businessName}}Async(Guid {{businessNameOfFirstLower}}Id);
}