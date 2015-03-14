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
            return View("Loading");
        }

        [AllowAnonymous]
        public ActionResult Shell()
        {
            return View();
        }

        //[AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
    }
}
