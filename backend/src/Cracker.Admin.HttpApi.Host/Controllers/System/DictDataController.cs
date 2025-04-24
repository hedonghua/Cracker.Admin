using Microsoft.AspNetCore.Mvc;
using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Cracker.Admin.Models;

namespace Cracker.Admin.Controllers.System
{
    [Route("api/[controller]")]
    public class DictDataController : AdminController
    {
        private readonly IDictDataService _dictService;

        public DictDataController(IDictDataService dictService)
        {
            _dictService = dictService;
        }

        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [AppBusinessLogFilter("新增字典")]
        [HasPermission("admin_system_dict_add")]
        public async Task<AppResponse> AddDictDataAsync(DictDataDto dto)
        {
            await _dictService.AddDictDataAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 字典分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [HasPermission("admin_system_dict_list")]
        public async Task<AppResponse<PagedResultDto<DictDataListDto>>> GetDictDataListAsync([FromQuery] DictDataQueryDto dto)
        {
            var data = await _dictService.GetDictDataListAsync(dto);
            return AppResponse.Data(data);
        }

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppBusinessLogFilter("修改字典")]
        [HasPermission("admin_system_dict_update")]
        public async Task<AppResponse> UpdateDictDataAsync(DictDataDto dto)
        {
            await _dictService.UpdateDictDataAsync(dto);
            return AppResponse.Ok();
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AppBusinessLogFilter("删除字典")]
        [HasPermission("admin_system_dict_delete")]
        public async Task<AppResponse> DeleteDictDataAsync([FromBody] Guid[] ids)
        {
            await _dictService.DeleteDictDataAsync(ids);
            return AppResponse.Ok();
        }
    }
}