using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Authorize()
        {
            var claims = new ClaimsPrincipal(User).Claims.ToArray();
            var identity = new ClaimsIdentity(claims, "Bearer");
            AuthenticationManager.SignIn(identity);
            return new EmptyResult();
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View(new LoginViewModel());
        }

        // GET: Account/Register
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }
    }
}