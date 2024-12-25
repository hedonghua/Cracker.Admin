using MediatR;

namespace Cracker.Admin.System.LogManagement.Commands
{
    public class AddLoginLogCommand : IRequest<bool>
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string? OperationMsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}