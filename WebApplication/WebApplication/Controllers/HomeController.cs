using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private bool IsInit {
            get { return Session["Init"] is bool && (bool)Session["Init"]; }
            set { Session["Init"] = value; }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
//            if (IsInit) return new EmptyResult();
//            IsInit = true;
            return View("Loading");
        }

        [AllowAnonymous]
        public ActionResult Shell()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }
    }
}
