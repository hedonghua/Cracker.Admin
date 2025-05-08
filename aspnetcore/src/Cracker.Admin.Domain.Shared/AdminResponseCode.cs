namespace Cracker.Admin;

public static class AdminResponseCode
{
    /// <summary>
    /// 成功
    /// </summary>
    public const int Success = 0;

    /// <summary>
    /// 失败
    /// </summary>
    public const int Fail = -1;

    /// <summary>
    /// 未登录
    /// </summary>
    public const int NoAuth = 401;

    /// <summary>
    /// 权限不足
    /// </summary>
    public const int Forbidden = 403;
}
