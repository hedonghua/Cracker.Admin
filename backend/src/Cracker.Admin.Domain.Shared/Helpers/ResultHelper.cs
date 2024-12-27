using Cracker.Admin.Core;
using Cracker.Admin.Models;

namespace Cracker.Admin.Helpers
{
    public class ResultHelper
    {
        public static IAppResult Ok(string? message = default)
        {
            return new AppResult() { Code = AdminResponseCode.Success, Message = message };
        }

        public static IAppResult<T> Ok<T>(T? data)
        {
            return new AppResult<T>() { Code = AdminResponseCode.Success, Data = data };
        }

        public static IAppResult Fail(string? message = default)
        {
            return new AppResult() { Code = AdminResponseCode.Fail, Message = message };
        }
    }
}