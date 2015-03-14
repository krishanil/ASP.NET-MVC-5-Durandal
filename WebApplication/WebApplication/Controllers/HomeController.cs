using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Shell");
        }

        //[AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
    }
}
