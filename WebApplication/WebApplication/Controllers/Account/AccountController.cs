using System.Web.Mvc;

namespace WebApplication.Controllers.Account
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ViewResult RegisterExternal()
        {
            return View();
        }
        
        public ViewResult Manage()
        {
            return View();
        }
    }
}