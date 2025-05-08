using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
