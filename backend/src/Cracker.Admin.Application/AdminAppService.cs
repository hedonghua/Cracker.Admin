using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Cracker.Admin;

/* Inherit your application services from this class.
 */
public abstract class AdminAppService : ApplicationService
{
    protected AdminAppService()
    {
    }
}
