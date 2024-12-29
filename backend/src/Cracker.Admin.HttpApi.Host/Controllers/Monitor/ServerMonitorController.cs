using Cracker.Admin.Filters;
using Cracker.Admin.Monitor;
using Cracker.Admin.Monitor.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.Monitor
{
    [Route("api/[controller]/[action]")]
    public class ServerMonitorController : AdminController
    {
        private readonly IServerMonitorService serverMonitorService;

        public ServerMonitorController(IServerMonitorService serverMonitorService)
        {
            this.serverMonitorService = serverMonitorService;
        }

        [HttpGet]
        [AppResultFilter]
        public Task<ServerMonitorInfoDto> GetServerMonitorInfoAsync()
        {
            return serverMonitorService.GetServerMonitorInfoAsync();
        }
    }
}