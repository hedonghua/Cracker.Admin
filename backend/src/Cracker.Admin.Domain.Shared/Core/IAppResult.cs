namespace Cracker.Admin.Core
{
    public interface IAppResult
    {
        int Code { get; set; }
        string? Message { get; set; }
        string? Status { get; set; }

        bool IsOk();
    }

    public interface IAppResult<T> : IAppResult
    {
        T? Data { get; set; }
    }
}