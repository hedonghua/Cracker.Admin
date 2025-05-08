namespace Cracker.Admin.Core
{
    public interface IPage
    {
        int Current { get; set; }
        int PageSize { get; set; }
    }
}