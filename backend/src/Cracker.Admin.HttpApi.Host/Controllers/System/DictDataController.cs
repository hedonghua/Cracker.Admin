using Microsoft.AspNetCore.Mvc;
using Cracker.Admin.Attributes;
using Cracker.Admin.Filters;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

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
        [AppResultFilter]
        [AppBusinessLogFilter("新增字典")]
        [HasPermission("admin_system_dict_add")]
        public Task<bool> AddDictDataAsync(DictDataDto dto) => _dictService.AddDictDataAsync(dto);

        /// <summary>
        /// 字典分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [HasPermission("admin_system_dict_list")]
        public Task<PagedResultDto<DictDataListDto>> GetDictDataListAsync([FromQuery] DictDataQueryDto dto) => _dictService.GetDictDataListAsync(dto);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [AppBusinessLogFilter("修改字典")]
        [HasPermission("admin_system_dict_update")]
        public Task<bool> UpdateDictDataAsync(DictDataDto dto) => _dictService.UpdateDictDataAsync(dto);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AppResultFilter]
        [AppBusinessLogFilter("删除字典")]
        [HasPermission("admin_system_dict_delete")]
        public Task<bool> DeleteDictDataAsync([FromBody] Guid[] ids) => _dictService.DeleteDictDataAsync(ids);
    }
}