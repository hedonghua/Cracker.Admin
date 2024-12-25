using System.Threading.Tasks;
using Cracker.Admin.Entity;

using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Database.DAO
{
    public interface ILoginLogDAO : ITransientDependency
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> WriteAsync(SysLoginLog entity);
    }
}