namespace Cracker.Admin.Attributes
{
    /// <summary>
    /// 必须主账户权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public sealed class MustMainPowerAttribute : Attribute
    {
    }
}