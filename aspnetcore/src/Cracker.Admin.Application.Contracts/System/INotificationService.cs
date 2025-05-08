using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System.Threading.Tasks;
using System;

namespace Cracker.Admin.System
{
    public interface INotificationService
    {
        Task AddNotificationAsync(NotificationDto dto);

        Task<PagedResultStruct<NotificationResultDto>> GetNotificationListAsync(NotificationSearchDto dto);

        Task UpdateNotificationAsync(NotificationDto dto);

        Task DeleteNotificationAsync(Guid[] ids);
    }
}