using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Cracker.Admin.Account
{
    public interface IAccountService : IApplicationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<LoginResultDto> LoginAsync(LoginDto dto);

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<TokenResultDto> GetAccessTokenAsync(string refreshToken);

        /// <summary>
        /// 当前用户信息、角色、权限
        /// </summary>
        /// <returns></returns>
        Task<UserInfoDto> GetUserInfoAsync();

        /// <summary>
        /// 获取当前用户可访问路由
        /// </summary>
        /// <returns></returns>
        Task<List<FrontRoute>> GetFrontRoutes(int listStruct = 0);

        /// <summary>
        /// 修改个人基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateUserInfoAsync(PersonalInfoDto dto);

        /// <summary>
        /// 修改个人密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateUserPwdAsync(UserPwdDto dto);

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        Task<bool> SignOutAsync();
    }
}