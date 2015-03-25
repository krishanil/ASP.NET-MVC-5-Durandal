using System.Web.Mvc;

namespace WebApplication.Controllers.Admin
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }
    }
}