namespace Cracker.Admin.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HasPermissionAttribute : Attribute
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string? Code => code;

        private readonly string code;

        public HasPermissionAttribute(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }
            this.code = code;
        }
    }
}