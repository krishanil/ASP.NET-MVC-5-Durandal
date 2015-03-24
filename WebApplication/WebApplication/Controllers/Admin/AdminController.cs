using System.Web.Mvc;

namespace WebApplication.Controllers.Admin
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }
    }
}