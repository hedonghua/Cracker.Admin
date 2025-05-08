using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cracker.Admin.Extensions
{
    public static class RedisExtension
    {
        public static async Task SetObjectAsync<T>(this IDatabase database, string key, T value, TimeSpan? expired = null)
        {
            if (value is string)
            {
                throw new NotSupportedException("请使用StringSetAsync方法");
            }

            var str = JsonSerializer.Serialize(value);
            await database.StringSetAsync(key, str, expired);
        }

        public static async Task<T?> GetObjectAsync<T>(this IDatabase database, string key)
        {
            if (!await database.KeyExistsAsync(key)) return default;

            var str = (await database.StringGetAsync(key)).ToString();
            if (string.IsNullOrEmpty(str)) return default;

            return JsonSerializer.Deserialize<T>(str);
        }

        public static async Task<RedisKey[]?> KeyPatternAsync(this IDatabase database, string pattern)
        {
            // 定义Lua脚本,调用Redis的KEYS命令
            var script = LuaScript.Prepare("local res = redis.call('KEYS', @keypattern) return res");

            // 执行脚本,传入匹配模式参数,获取结果
            var result = await database.ScriptEvaluateAsync(script, new { @keypattern = pattern });
            return ((RedisKey[]?)result);
        }

        public static async Task KeyDeleteByPatternAsync(this IDatabase database, string pattern)
        {
            var keys = await database.KeyPatternAsync(pattern);
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    await database.KeyDeleteAsync(key);
                }
            }
        }
    }
}