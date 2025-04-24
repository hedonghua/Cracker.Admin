using Cracker.Admin.Core;
using Cracker.Admin.Models;

namespace Cracker.Admin.Helpers
{
    public class ResultHelper
    {
        public static IAppResponse Ok(string? message = default)
        {
            return new AppResponse() { Code = AdminResponseCode.Success, Message = message };
        }

        public static IAppResponse<T> Ok<T>(T? data)
        {
            return new AppResponse<T>() { Code = AdminResponseCode.Success, Data = data };
        }

        public static IAppResponse Fail(string? message = default)
        {
            return new AppResponse() { Code = AdminResponseCode.Fail, Message = message };
        }
    }
}