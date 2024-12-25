using System;
using System.Collections.Generic;
using System.Text;

namespace Cracker.Admin.CustomAttrs
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PermissionAttribute : Attribute
    {
        /// <summary>
        /// 权限字符串
        /// </summary>
        public string? Str { get; private set; }

        public PermissionAttribute(string? str)
        {
            Str = str;
        }
    }
}