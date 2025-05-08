using Cracker.Admin.Entities;
using Cracker.Admin.Extensions;
using Cracker.Admin.Models;
using Cracker.Admin.System.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Cracker.Admin.System
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> repository;
        private readonly IRepository<OrgEmployee> employeeRepository;

        public NotificationService(IRepository<Notification> repository, IRepository<OrgEmployee> employeeRepository)
        {
            this.repository = repository;
            this.employeeRepository = employeeRepository;
        }

        public Task AddNotificationAsync(NotificationDto dto)
        {
            var entity = new Notification()
            {
                Title = dto.Title,
                Description = dto.Description,
                EmployeeId = dto.EmployeeId,
                IsReaded = false
            };
            return repository.InsertAsync(entity);
        }

        public Task DeleteNotificationAsync(Guid[] ids)
        {
            return repository.DeleteDirectAsync(x => ids.Contains(x.Id));
        }

        public async Task<PagedResultStruct<NotificationResultDto>> GetNotificationListAsync(NotificationSearchDto dto)
        {
            var query = from n in await repository.GetQueryableAsync()
                        join e in await employeeRepository.GetQueryableAsync() on n.EmployeeId equals e.Id
                        select new NotificationResultDto
                        {
                            Id = n.Id,
                            Title = n.Title,
                            Description = n.Description,
                            EmployeeId = n.EmployeeId,
                            IsReaded = n.IsReaded,
                            CreationTime = n.CreationTime,
                            ReadedTime = n.ReadedTime,
                            EmployeeName = e.Name
                        };
            query = query.WhereIf(!string.IsNullOrEmpty(dto.Title), x => x.Title!.Contains(x.Title))
                .WhereIf(dto.IsReaded.HasValue, x => x.IsReaded == dto.IsReaded);
            var list = query.StartPage(dto).ToList();
            var total = query.Count();
            return new PagedResultStruct<NotificationResultDto>(dto, total, list);
        }

        public async Task UpdateNotificationAsync(NotificationDto dto)
        {
            var entity = await repository.GetAsync(x => x.Id == dto.Id);
            if (entity.IsReaded)
            {
                throw new BusinessException(message: "已读消息不能修改");
            }
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.EmployeeId = dto.EmployeeId;
            await repository.UpdateAsync(entity);
        }
    }
}