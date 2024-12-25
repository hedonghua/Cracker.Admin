using System.Text.Json.Serialization;

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
            Code = flag ? AdminDomainErrorCodes.SUCCESS : AdminDomainErrorCodes.FAIL;
        }

        public bool IsOk()
        {
            return Code == AdminDomainErrorCodes.SUCCESS;
        }
    }

    public class AppResult<T> : AppResult
    {
        [JsonPropertyOrder(2)]
        public T? Data { get; set; }

        public AppResult()
        {
            Code = AdminDomainErrorCodes.SUCCESS;
        }

        public AppResult(T? data)
        {
            Code = AdminDomainErrorCodes.SUCCESS;
            Data = data;
        }
    }
}