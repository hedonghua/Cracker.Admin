using Cracker.Admin.Services;
using Cracker.Admin.System.LogManagement.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers
{
    /// <summary>
    /// TODO: 测试控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class TestController : AbpControllerBase
    {
        private readonly MqttService mqttSingletonService;

        public TestController(MqttService mqttSingletonService)
        {
            this.mqttSingletonService = mqttSingletonService;
        }

        [HttpPost]
        public async Task<string> PushByTopicHelloAsync()
        {
            await mqttSingletonService.PushAsync("hello", new LoginLogQueryDto { Page = 1, Size = 12 });
            return "推送成功";
        }
    }
}