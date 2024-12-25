using Microsoft.Extensions.Logging;
using System;
using Volo.Abp;
using Volo.Abp.Logging;

namespace Cracker.Admin.MyExceptions
{
    /// <summary>
    /// 业务/提示异常，手动抛出
    /// </summary>
    public class TipException : Exception
    {
        public TipException(string message) : base(message)
        {
        }
    }
}