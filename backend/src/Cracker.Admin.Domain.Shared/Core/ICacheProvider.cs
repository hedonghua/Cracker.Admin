using System.Threading.Tasks;

namespace Cracker.Admin.Core
{
    public interface ICacheProvider
    {
        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>包含指定键的值的任务</returns>
        Task<TValue?> GetAsync<TValue>(string key);

        /// <summary>
        /// 设置指定键的值，并可以选择设置过期时间
        /// </summary>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">要设置的值</param>
        /// <param name="slidingExpiration">滑动过期时间，可选</param>
        /// <param name="absoluteExpiration">绝对过期时间，可选</param>
        /// <returns>表示操作的任务</returns>
        Task SetAsync<TValue>(string key, TValue value, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null);

        /// <summary>
        /// 移除指定键的缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>表示操作的任务</returns>
        Task DelAsync(string key);

        /// <summary>
        /// 检查指定键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>表示操作的任务，任务结果为是否存在</returns>
        Task<bool> ExistsAsync(string key);

        Task HSetAsync<T>(string key, string field, T value);

        Task HDelAsync(string key, string field);

        Task<T?> HGetAsync<T>(string key, string field);

        Task IncrByAsync(string key, int value);
    }
}