using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cracker.Admin.Services
{
    public class MqttService : ISingletonDependency
    {
        private readonly MqttServer mqttServer;
        private readonly IConfiguration configuration;
        private readonly IDatabase redisDb;

        public MqttService(MqttServer mqttServer, IConfiguration configuration, IDatabase redisDb)
        {
            this.mqttServer = mqttServer;
            this.configuration = configuration;
            this.redisDb = redisDb;
        }

        public async Task ValidatingConnectionAsync(ValidatingConnectionEventArgs e)
        {
            var isValidToken = await redisDb.KeyExistsAsync($"MqttToken:{e.UserName}");
            var isValidAccount = e.UserName == configuration["Mqtt:UserName"] && e.Password == configuration["Mqtt:Password"];
            if (!(isValidToken || isValidAccount))
            {
                e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                return;
            }
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