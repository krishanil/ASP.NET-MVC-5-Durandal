using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApplication.BLL.Managers.Account;
using WebApplication.BLL.Models.Account.BindingModels;
using WebApplication.BLL.Models.Account.ViewModels;
using WebApplication.Results;

namespace WebApplication.Controllers.Account
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountApiController : ApiController
    {
        private IAccountManager AccountManager { get; set; }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        public AccountApiController(IAccountManager accountManager, IAuthenticationManager authenticationManager, AppUserManager appUserManager) : this(Startup.OAuthOptions.AccessTokenFormat, accountManager, authenticationManager, appUserManager) { }

        public AccountApiController(ISecureDataFormat<AuthenticationTicket> accessTokenFormat, IAccountManager accountManager, IAuthenticationManager authenticationManager, AppUserManager appUserManager)
        {
            AccessTokenFormat = accessTokenFormat;
            AccountManager = accountManager;
            AccountManager.AuthenticationManager = authenticationManager;
            AccountManager.UserManager = appUserManager;
            AccountManager.UrlManager = Url;
            AccountManager.StartupPublicClientId = Startup.PublicClientId;
        }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoModel GetUserInfo()
        {
            return AccountManager.GetUserInfo(User);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AccountManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            return await AccountManager.GetManageInfo(User, returnUrl, generateState);
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            return await RunTask(() => AccountManager.ChangePasswordAsync(User, model));
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordModel model)
        {
            return await RunTask(() => AccountManager.AddPasswordAsync(User, model));
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginModel model)
        {
            return await RunTask(() => AccountManager.AddExternalLogin(User, model, AccessTokenFormat.Unprotect(model.ExternalAccessToken)));
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginModel model)
        {
            return await RunTask(() => AccountManager.RemoveLogin(User, model));
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null) return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));

            if (!User.Identity.IsAuthenticated) return new ChallengeResult(provider, this);

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null) return InternalServerError();

            if (externalLogin.LoginProvider != provider)
            {
                AccountManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            AccountManager.SetExternalLogin(externalLogin, OAuthDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            return AccountManager.GetExternalLogins(returnUrl.IsNullOrWhiteSpace() ? new Uri(Request.RequestUri, returnUrl).AbsoluteUri : returnUrl, generateState);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            return await RunTask(() => AccountManager.CreateAsync(model));
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalModel model)
        {
            return await RunTask(() => AccountManager.RegisterExternal(model));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) AccountManager.UserManagerDispose(true);
            base.Dispose(disposing);
        }

        private async Task<IHttpActionResult> RunTask(Func<Task<IdentityResult>> function)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await function();

            if (result == null) return InternalServerError();

            if (!result.Errors.Any()) return Ok();
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return BadRequest(ModelState);
        }
    }
}
