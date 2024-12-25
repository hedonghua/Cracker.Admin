using System.Threading.Tasks;

namespace Cracker.Admin.Core
{
    public interface IKeySettings
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="refresh">开启刷新</param>
        /// <returns></returns>
        Task InitializationAsync(bool refresh = false);

        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 更新字典值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isDbSync">是否同步数据库</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string value, bool isDbSync = false);

        /// <summary>
        /// 移除字典
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);
    }
}