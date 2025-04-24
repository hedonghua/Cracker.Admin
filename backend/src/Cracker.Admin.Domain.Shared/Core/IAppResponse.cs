namespace Cracker.Admin.Core
{
    public interface IAppResponse
    {
        int Code { get; set; }
        string? Message { get; set; }
        string? Status { get; set; }

        bool IsOk();
    }

    public interface IAppResponse<T> : IAppResponse
    {
        T? Data { get; set; }
    }
}