using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Services
{
    public class MqttSingletonService : ISingletonDependency
    {
        private MqttServer? _server;
        private static readonly object _serverLock = new();
        private readonly IConfiguration configuration;

        public MqttSingletonService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// MQTT Server单例对象
        /// </summary>
        public MqttServer Server
        {
            get
            {
                if (_server == null)
                {
                    lock (_serverLock)
                    {
                        _server ??= CreateMqttServer();
                    }
                }

                return _server;
            }
        }

        private MqttServer CreateMqttServer()
        {
            var mqttServerFactory = new MqttServerFactory();

            var mqttServerOptions = mqttServerFactory.CreateServerOptionsBuilder()
                .WithDefaultEndpoint().WithDefaultEndpointPort(int.Parse(configuration["Mqtt:Port"]!))
                .Build();

            var server = mqttServerFactory.CreateMqttServer(mqttServerOptions);

            server.ValidatingConnectionAsync += e =>
            {
                if (e.UserName != configuration["Mqtt:UserName"])
                {
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                }

                if (e.Password != configuration["Mqtt:Password"])
                {
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                }

                return Task.CompletedTask;
            };

            return server;
        }

        /// <summary>
        /// 以指定主题推送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> PushAsync<T>(string topic, T? payload = default)
        {
            if (!Server.IsStarted)
            {
                await Server.StartAsync();
            }

            var payloadString = string.Empty;
            if (payload != null)
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                payloadString = JsonConvert.SerializeObject(payload, settings);
            }
            var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payloadString).Build();

            await Server.InjectApplicationMessage(
                new InjectedMqttApplicationMessage(message)
                {
                    SenderClientId = "Cracker.Admin"
                });

            return true;
        }
    }
}