using Cracker.Admin.Core;
using Cracker.Admin.Helpers;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Cracker.Admin.Controllers.Account
{
    [Route("api/[controller]/[action]")]
    public class MqttController : AdminController
    {
        private readonly IDatabase redisDb;

        public MqttController(IDatabase redisDb)
        {
            this.redisDb = redisDb;
        }

        [HttpPost]
        public async Task<IAppResponse> GetMqttTokenAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentNullException(nameof(code));

            var codeKey = $"MqttTokenCode:{code}";

            if (await redisDb.KeyExistsAsync(codeKey))
            {
                var ttl = redisDb.KeyTimeToLive(codeKey)?.TotalSeconds ?? 0;
                if (ttl > 60)
                {
                    return ResultHelper.Ok(new { token = redisDb.StringGet(codeKey).ToString(), expired = TimeHelper.GetCurrentTimestamp() + (int)Math.Floor(ttl) });
                }
            }

            var token = Guid.NewGuid().ToString();
            var expired = TimeHelper.GetCurrentTimestamp() + 3600;
            await redisDb.StringSetAsync($"MqttToken:{token}", expired, TimeSpan.FromHours(1), true);
            await redisDb.StringSetAsync(codeKey, token, TimeSpan.FromHours(1));
            return ResultHelper.Ok(new { expired = expired, token });
        }
    }
}