using System.Text.Json.Serialization;

using Cracker.Admin.Core;

namespace Cracker.Admin.Models
{
    public class AppResponse : IAppResponse
    {
        [JsonPropertyOrder(0)]
        public int Code { get; set; }

        [JsonPropertyOrder(1)]
        public string? Message { get; set; }

        [JsonPropertyOrder(2)]
        public string? Status { get; set; }

        public AppResponse()
        { }

        public AppResponse(int code, string? msg)
        {
            Code = code;
            Message = msg;
        }

        public AppResponse(bool flag)
        {
            Code = flag ? AdminResponseCode.Success : AdminResponseCode.Fail;
        }

        public bool IsOk()
        {
            return Code == AdminResponseCode.Success;
        }

        public static AppResponse Ok(string? msg = default)
        {
            return new AppResponse
            {
                Code = AdminResponseCode.Success,
                Message = msg
            };
        }

        public static AppResponse Fail(string? msg = default)
        {
            return new AppResponse
            {
                Code = AdminResponseCode.Fail,
                Message = msg
            };
        }

        public static AppResponse<T> Data<T>(T? data)
        {
            return new AppResponse<T>(data);
        }
    }

    public class AppResponse<T> : AppResponse, IAppResponse<T>
    {
        [JsonPropertyOrder(3)]
        public T? Data { get; set; }

        public AppResponse()
        {
            Code = AdminResponseCode.Success;
        }

        public AppResponse(T? data)
        {
            Code = AdminResponseCode.Success;
            Data = data;
        }
    }
}