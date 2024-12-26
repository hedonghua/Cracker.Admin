using Cracker.Admin.Core;
using Cracker.Admin.Models;

namespace Cracker.Admin.Helpers
{
    public class ResultHelper
    {
        public static IAppResult Ok(string? message = default)
        {
            return new AppResult() { Code = AdminDomainErrorCodes.SUCCESS, Message = message };
        }

        public static IAppResult<T> Ok<T>(T? data)
        {
            return new AppResult<T>() { Code = AdminDomainErrorCodes.SUCCESS, Data = data };
        }

        public static IAppResult Fail(string? message = default)
        {
            return new AppResult() { Code = AdminDomainErrorCodes.FAIL, Message = message };
        }
    }
}