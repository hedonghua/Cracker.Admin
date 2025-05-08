using Cracker.Admin.Attributes;
using Cracker.Admin.Core;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class DictTypeController : AbpControllerBase
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
    [HasPermission("Sys.DictType.Add")]
    public async Task<IAppResponse> AddDictTypeAsync([FromBody] DictTypeDto dto)
    {
        await _dictTypeService.AddDictTypeAsync(dto);
        return A.Ok();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [HasPermission("Sys.DictType.List")]
    public async Task<AppResponse<PagedResultStruct<DictTypeResultDto>>> GetDictTypeListAsync([FromQuery] DictTypeSearchDto dto)
    {
        var data = await _dictTypeService.GetDictTypeListAsync(dto);
        return A.Data(data);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut]
    [HasPermission("Sys.DictType.Update")]
    public async Task<IAppResponse> UpdateDictTypeAsync([FromBody] DictTypeDto dto)
    {
        await _dictTypeService.UpdateDictTypeAsync(dto);
        return A.Ok();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="dictType"></param>
    /// <returns></returns>
    [HttpDelete("{dictType}")]
    [HasPermission("Sys.DictType.Delete")]
    public async Task<IAppResponse> DeleteDictTypeAsync(string dictType)
    {
        await _dictTypeService.DeleteDictTypeAsync(dictType);
        return A.Ok();
    }

    /// <summary>
    /// 字典选项
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResponse<List<AppOption>>> GetDictDataOptionsAsync(string type)
    {
        var data = await _dictTypeService.GetDictDataOptionsAsync(type);
        return A.Data(data);
    }
}