using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet.Server;
using System.Threading;
using System.Threading.Tasks;

namespace Cracker.Admin.Services
{
    public class MqttServerHostService : IHostedService
    {
        private readonly ILogger<MqttServerHostService> logger;
        private readonly MqttSingletonService mqttService;

        public MqttServerHostService(ILogger<MqttServerHostService> logger, MqttSingletonService mqttService)
        {
            this.logger = logger;
            this.mqttService = mqttService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!mqttService.Server.IsStarted)
            {
                await mqttService.Server.StartAsync();
            }

            logger.LogInformation("MQTT Server启动成功");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await mqttService.Server.StopAsync();
        }
    }
}