using System.Text.Json.Serialization;

using Cracker.Admin.Core;

namespace Cracker.Admin.Models
{
    public class AppResult : IAppResult
    {
        [JsonPropertyOrder(0)]
        public int Code { get; set; }

        [JsonPropertyOrder(1)]
        public string? Message { get; set; }

        public AppResult()
        { }

        public AppResult(int code, string? msg)
        {
            Code = code;
            Message = msg;
        }

        public AppResult(bool flag)
        {
            Code = flag ? AdminResponseCode.Success : AdminResponseCode.Fail;
        }

        public bool IsOk()
        {
            return Code == AdminResponseCode.Success;
        }
    }

    public class AppResult<T> : AppResult, IAppResult<T>
    {
        [JsonPropertyOrder(2)]
        public T? Data { get; set; }

        public AppResult()
        {
            Code = AdminResponseCode.Success;
        }

        public AppResult(T? data)
        {
            Code = AdminResponseCode.Success;
            Data = data;
        }
    }
}