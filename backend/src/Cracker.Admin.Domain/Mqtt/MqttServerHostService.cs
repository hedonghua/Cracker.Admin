//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using MQTTnet;
//using MQTTnet.Server;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Cracker.Admin.Mqtt
//{
//    public class MqttServerHostService : IHostedService, IDisposable
//    {
//        public static MqttServer? _mqttServer = null;
//        private readonly ILogger<MqttServerHostService> _logger;

//        public MqttServerHostService(ILogger<MqttServerHostService> logger)
//        {
//            _logger = logger;
//        }

//        private Task _mqttServer_ClientConnectedAsync(ClientConnectedEventArgs args)
//        {
//            _logger.LogInformation("客户端ID=【{ClientId}】已连接，用户名=【{UserName}】，地址=【{Endpoint}】", args.ClientId, args.UserName, args.Endpoint);
//            return Task.CompletedTask;
//        }

//        private Task _mqttServer_ClientDisconnectedAsync(ClientDisconnectedEventArgs args)
//        {
//            _logger.LogInformation("客户端ID=【{ClientId}】已断开，地址=【{Endpoint}】", args.ClientId, args.Endpoint);
//            return Task.CompletedTask;
//        }

//        private Task _mqttServer_StartedAsync(EventArgs args) 
//        {
//            _logger.LogInformation("mqtt服务已启动...");
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _mqttServer?.Dispose();
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            MqttServerOptionsBuilder optionsBuilder = new MqttServerOptionsBuilder();
//            optionsBuilder.WithDefaultEndpointPort(10086); //服务端口
//            optionsBuilder.WithConnectionBacklog(1000); //最大连接数

//            MqttServerOptions options = optionsBuilder.Build();
//            _mqttServer = new MqttFactory().CreateMqttServer(options);

//            _mqttServer.ClientConnectedAsync += _mqttServer_ClientConnectedAsync;
//            _mqttServer.ClientDisconnectedAsync += _mqttServer_ClientDisconnectedAsync;
//            _mqttServer.StartedAsync += _mqttServer_StartedAsync;

//            await _mqttServer.StartAsync();
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}