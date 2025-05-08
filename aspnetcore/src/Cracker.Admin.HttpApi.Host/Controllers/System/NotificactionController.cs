using System;
using System.Threading.Tasks;

using Cracker.Admin.Attributes;
using Cracker.Admin.Models;
using Cracker.Admin.System;
using Cracker.Admin.System.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.System
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotificactionController : AbpControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificactionController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        [HasPermission("Sys.Notification.Add")]
        public async Task<AppResponse> AddNotificationAsync([FromBody] NotificationDto dto)
        {
            await notificationService.AddNotificationAsync(dto);
            return A.Ok();
        }

        [HttpGet]
        [HasPermission("Sys.Notification.List")]
        public async Task<AppResponse<PagedResultStruct<NotificationResultDto>>> GetNotificationListAsync([FromQuery] NotificationSearchDto dto)
        {
            var data = await notificationService.GetNotificationListAsync(dto);
            return A.Data(data);
        }

        [HttpGet]
        [HasPermission("Sys.Notification.Update")]
        public async Task<AppResponse> UpdateNotificationAsync([FromBody] NotificationDto dto)
        {
            await notificationService.UpdateNotificationAsync(dto);
            return A.Ok();
        }

        [HttpDelete]
        [HasPermission("Sys.Notification.Delete")]
        public async Task<AppResponse> DeleteNotificationAsync([FromBody] Guid[] ids)
        {
            await notificationService.DeleteNotificationAsync(ids);
            return A.Ok();
        }
    }
}