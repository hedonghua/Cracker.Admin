using MQTTnet;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Services
{
    public class MqttService : ISingletonDependency
    {
        private readonly MqttServer mqttServer;

        public MqttService(MqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
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

            await mqttServer.InjectApplicationMessage(
                new InjectedMqttApplicationMessage(message)
                {
                    SenderClientId = "cracker_admin_" + Guid.NewGuid().ToString("N")
                });

            return true;
        }
    }
}