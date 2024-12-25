using System;
using System.Collections.Generic;
using System.Text;

namespace Cracker.Admin.Models
{
    public interface IAppResult
    {
        int Code { get; set; }
        string? Message { get; set; }

        bool IsOk();
    }
}