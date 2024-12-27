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
    [Route("api/dict")]
    public class DictController : AdminController
    {
        private readonly IDictService _dictService;

        public DictController(IDictService dictService)
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
        public Task<bool> AddDictAsync(DictDto dto) => _dictService.AddDictAsync(dto);

        /// <summary>
        /// 字典分页列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [AppResultFilter]
        [HasPermission("admin_system_dict_list")]
        public Task<PagedResultDto<DictListDto>> GetDictListAsync([FromQuery] DictQueryDto dto) => _dictService.GetDictListAsync(dto);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [AppResultFilter]
        [AppBusinessLogFilter("修改字典")]
        [HasPermission("admin_system_dict_update")]
        public Task<bool> UpdateDictAsync(DictDto dto) => _dictService.UpdateDictAsync(dto);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [AppResultFilter]
        [AppBusinessLogFilter("删除字典")]
        [HasPermission("admin_system_dict_delete")]
        public Task<bool> DeleteDictAsync([FromBody] Guid[] ids) => _dictService.DeleteDictAsync(ids);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        [AppResultFilter]
        [AppBusinessLogFilter("刷新字典缓存")]
        [HasPermission("admin_system_dict_refresh")]
        public Task<bool> RefreshCacheAsync() => _dictService.RefreshCacheAsync();
    }
}