using Cracker.Admin.Monitor.Dtos;
using System.Threading.Tasks;

namespace Cracker.Admin.Monitor
{
    public interface IServerMonitorService
    {
        Task<ServerMonitorInfoDto> GetServerMonitorInfoAsync();
    }
}