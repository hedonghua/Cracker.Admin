using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.{{moduleName}};
using Cracker.Admin.{{moduleName}}.Dtos;
using Microsoft.AspNetCore.Mvc;
using {{moduleName}};
using {{moduleName}}.Threading.Tasks;

namespace Cracker.Admin.Controllers.{{moduleName}};

[Route("api/[controller]/[action]")]
public class {{businessName}}Controller : AdminController
{
    private readonly I{{businessName}}Service _{{businessNameOfFirstLower}}Service;

    public {{businessName}}Controller(I{{businessName}}Service {{businessNameOfFirstLower}}Service)
    {
        _{{businessNameOfFirstLower}}Service = {{businessNameOfFirstLower}}Service;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IAppResult> Add{{businessName}}Async([FromBody] {{businessName}}Dto dto)
    {
        await _{{businessNameOfFirstLower}}Service.Add{{businessName}}Async(dto);
        return ResultHelper.Ok();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IAppResult> Get{{businessName}}ListAsync([FromQuery] {{businessName}}SearchDto dto)
    {
        var data = await _{{businessNameOfFirstLower}}Service.Get{{businessName}}ListAsync(dto);
        return ResultHelper.Ok(data);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IAppResult> Update{{businessName}}Async([FromBody] {{businessName}}Dto dto)
    {
        await _{{businessNameOfFirstLower}}Service.Update{{businessName}}Async(dto);
        return ResultHelper.Ok();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    public async Task<bool> Delete{{businessName}}Async(Guid id)
    {
        await _{{businessNameOfFirstLower}}Service.Delete{{businessName}}Async(id);
        return ResultHelper.Ok(); 
    }
}