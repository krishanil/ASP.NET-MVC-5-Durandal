using System.Web.Mvc;

namespace WebApplication.Controllers
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