using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountApiController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountApiController() { }

        public AccountApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: api/AccountApi
        [AllowAnonymous]
        public bool Get()
        {
            return User.Identity.IsAuthenticated;
        }

        // GET: api/AccountApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AccountApi
        [AllowAnonymous]
        public async Task<object> Post(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = SignInStatus.Failure, user = ClientUserData(User) });
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            return Json(new { result, user = ClientUserData(User) });
        }

        // PUT: api/AccountApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AccountApi/5
        public void Delete(int id)
        {
        }

        private object ClientUserData(IPrincipal user)
        {
            return new
            {
                user.Identity.Name,
                user.Identity.IsAuthenticated
            };
        }
    }
}
