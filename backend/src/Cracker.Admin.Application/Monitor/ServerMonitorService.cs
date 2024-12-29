using Cracker.Admin.Monitor.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.Monitor
{
    public class ServerMonitorService : ApplicationService, IServerMonitorService
    {
        public async Task<ServerMonitorInfoDto> GetServerMonitorInfoAsync()
        {
            var model = new ServerMonitorInfoDto()
            {
                Dotnet = new Dictionary<string, string>()
                {
                    ["version"] = ".net" + Environment.Version.ToString()
                },
                Server = new Dictionary<string, string>()
                {
                    ["machineName"] = Environment.MachineName,
                    ["osVersion"] = Environment.OSVersion.ToString(),
                    ["systemDirectory"] = Environment.SystemDirectory
                }
            };

            return await Task.FromResult(model);
        }
    }
}