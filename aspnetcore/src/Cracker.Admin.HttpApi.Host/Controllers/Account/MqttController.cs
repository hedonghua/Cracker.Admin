using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Account
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MqttController : AbpControllerBase
    {
        private readonly IDatabase redisDb;

        public MqttController(IDatabase redisDb)
        {
            this.redisDb = redisDb;
        }

        [HttpPost]
        public async Task<AppResponse<MqttToken>> GetMqttTokenAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentNullException(nameof(code));

            var codeKey = $"MqttTokenCode:{code}";

            if (await redisDb.KeyExistsAsync(codeKey))
            {
                var ttl = redisDb.KeyTimeToLive(codeKey)?.TotalSeconds ?? 0;
                if (ttl > 60)
                {
                    return A.Data(new MqttToken { Token = redisDb.StringGet(codeKey).ToString(), Expired = TimeHelper.GetCurrentTimestamp() + (int)Math.Floor(ttl) });
                }
            }

            var token = Guid.NewGuid().ToString();
            var expired = TimeHelper.GetCurrentTimestamp() + 3600;
            await redisDb.StringSetAsync($"MqttToken:{token}", expired, TimeSpan.FromHours(1), true);
            await redisDb.StringSetAsync(codeKey, token, TimeSpan.FromHours(1));
            return A.Data(new MqttToken { Expired = expired, Token = token });
        }
    }

    public class MqttToken
    {
        public long Expired { get; set; }
        public string? Token { get; set; }
    }
}