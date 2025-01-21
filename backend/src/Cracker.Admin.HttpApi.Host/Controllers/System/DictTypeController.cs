using System;
using System.Threading.Tasks;

using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;

using Microsoft.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System;

[Route("api/[controller]/[action]")]
public class DictTypeController : AdminController
{
    private readonly IDictTypeService _dictTypeService;

    public DictTypeController(IDictTypeService dictTypeService)
    {
        _dictTypeService = dictTypeService;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IAppResult> AddDictTypeAsync([FromBody] DictTypeDto dto)
    {
        await _dictTypeService.AddDictTypeAsync(dto);
        return ResultHelper.Ok();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IAppResult> GetDictTypeListAsync([FromQuery] DictTypeSearchDto dto)
    {
        var data = await _dictTypeService.GetDictTypeListAsync(dto);
        return ResultHelper.Ok(data);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IAppResult> UpdateDictTypeAsync([FromBody] DictTypeDto dto)
    {
        await _dictTypeService.UpdateDictTypeAsync(dto);
        return ResultHelper.Ok();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{dictType}")]
    public async Task<IAppResult> DeleteDictTypeAsync(string dictType)
    {
        await _dictTypeService.DeleteDictTypeAsync(dictType);
        return ResultHelper.Ok();
    }

    /// <summary>
    /// 字典选项
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IAppResult> GetDictDataOptionsAsync(string type)
    {
        var data = await _dictTypeService.GetDictDataOptionsAsync(type);
        return ResultHelper.Ok(data);
    }
}