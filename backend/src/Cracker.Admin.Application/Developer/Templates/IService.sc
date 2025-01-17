using Cracker.Admin.Models;
using Cracker.Admin.{{moduleName}}.Dtos;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.Application.Contracts.{{moduleName}};

public interface I{{businessName}}Service
{
    Task Add{{businessName}}Async({{businessName}}Dto dto);

    Task<PagedResultStruct<{{businessName}}ResultDto>> Get{{businessName}}ListAsync({{businessName}}SearchDto dto);

    Task Update{{businessName}}Async({{businessName}}Dto dto);

    Task Delete{{businessName}}Async(Guid {{businessNameOfFirstLower}}Id);
}