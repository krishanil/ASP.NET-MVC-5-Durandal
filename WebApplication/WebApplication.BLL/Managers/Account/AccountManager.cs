using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Owin;
using WebApplication.BLL.Models.BindingModels;
using WebApplication.BLL.Models.ViewModels;
using WebApplication.BLL.Providers;
using WebApplication.DAL.DataContext.AccountContext;
using WebApplication.DAL.Repositories.AccountRepository;

namespace WebApplication.BLL.Managers.Account
{
    public class AccountManager : IAccountManager
    {
        private const string LocalLoginProvider = "Local";
        private const string DefaultUserRole = "RegisteredUsers";

        public UrlHelper UrlManager { get; set; }

        public string StartupPublicClientId { get; set; }

        private IAccountRepository Repository { get; set; }

        public AppUserManager UserManager { get; set; }

        public IAuthenticationManager AuthenticationManager { get; set; }

        public AccountManager(IAccountRepository repository)
        {
            Repository = repository;
        }

        public void SetOwinContext(IAppBuilder app)
        {
            app.CreatePerOwinContext(Repository.Context.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
        }

        public UserInfoModel GetUserInfo(IPrincipal user)
        {
            var externalLogin = ExternalLoginData.FromIdentity(user.Identity as ClaimsIdentity);

            var roleClaimValues = ((ClaimsIdentity)user.Identity).FindAll(ClaimTypes.Role).Select(c => c.Value);

            var roles = string.Join(",", roleClaimValues);

            return new UserInfoModel
            {
                UserName = user.Identity.GetUserName(),
                Email = ((ClaimsIdentity)user.Identity).FindFirstValue(ClaimTypes.Email),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null,
                UserRoles = roles
            };
        }

        public async Task<ManageInfoModel> GetManageInfo(IPrincipal user, string returnUrl, bool generateState = false)
        {
            IdentityUser appUser = await UserManager.FindByIdAsync(user.Identity.GetUserId());

            if (appUser == null) return null;

            var logins = appUser.Logins.Select(linkedAccount => new UserLoginInfoModel
            {
                LoginProvider = linkedAccount.LoginProvider,
                ProviderKey = linkedAccount.ProviderKey

            }).ToList();

            if (appUser.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = appUser.UserName,
                });
            }

            return new ManageInfoModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = appUser.Email,
                UserName = appUser.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        public IEnumerable<ExternalLoginModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            string state = null;

            var descriptions = AuthenticationManager.GetExternalAuthenticationTypes();

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }

            return descriptions.Select(description => new ExternalLoginModel
            {
                State = state,
                Name = description.Caption,
                
                Url = UrlManager.Route("ExternalLogin", new
                {
                    provider = description.AuthenticationType,
                    response_type = "token",
                    client_id = StartupPublicClientId,
                    redirect_uri = returnUrl,
                    state
                }),

            }).ToList();
        }

        public void SignOut(string authenticationType)
        {
            AuthenticationManager.SignOut(authenticationType);
        }

        public async Task<IdentityResult> ChangePasswordAsync(IPrincipal user, ChangePasswordModel model)
        {
            return await UserManager.ChangePasswordAsync(user.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        }

        public async Task<IdentityResult> AddPasswordAsync(IPrincipal user, SetPasswordModel model)
        {
            return await UserManager.AddPasswordAsync(user.Identity.GetUserId(), model.NewPassword);
        }

        public async Task<IdentityResult> AddExternalLogin(IPrincipal user, AddExternalLoginModel model, AuthenticationTicket ticket)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return new IdentityResult("External login failure.");
            }

            var externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null) return new IdentityResult("The external login is already associated with an account.");

            return await UserManager.AddLoginAsync(user.Identity.GetUserId(), new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));
        }

        public async Task<IdentityResult> RemoveLogin(IPrincipal user, RemoveLoginModel model)
        {
            if (model.LoginProvider == LocalLoginProvider) return await UserManager.RemovePasswordAsync(user.Identity.GetUserId());
            
            return await UserManager.RemoveLoginAsync(user.Identity.GetUserId(), new UserLoginInfo(model.LoginProvider, model.ProviderKey));
        }

        public async void SetExternalLogin(ExternalLoginData externalLogin, string authenticationType, string cookies)
        {
            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            var hasRegistered = user != null;

            if (hasRegistered)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager, authenticationType);
                var cookieIdentity = await user.GenerateUserIdentityAsync(UserManager, cookies);
                var properties = AuthenticationPropertiesConfig.CreateProperties(oAuthIdentity);

                AuthenticationManager.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, authenticationType);

                AuthenticationManager.SignIn(identity);
            }
        }

        public async Task<IdentityResult> CreateAsync(RegisterModel model)
        {
            var user = new AppUser { UserName = model.UserName, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return result;
            return await UserManager.AddToRoleAsync(user.Id, DefaultUserRole);
        }

        public async Task<IdentityResult> RegisterExternal(RegisterExternalModel model)
        {
            var info = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (info == null) return new IdentityResult("External login info not found.");

            var user = new AppUser { UserName = model.UserName, Email = model.Email ?? "" };

            var result = await UserManager.CreateAsync(user);

            if (!result.Succeeded) return result;

            result = await UserManager.AddLoginAsync(user.Id, info.Login);

            if (!result.Succeeded) return result;

            return await UserManager.AddToRoleAsync(user.Id, DefaultUserRole);
        }

        public void UserManagerDispose(bool disposing)
        {
            UserManager.Dispose();
        }

        private static class RandomOAuthStateGenerator
        {
            private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0) throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");

                var strengthInBytes = strengthInBits / bitsPerByte;

                var data = new byte[strengthInBytes];
                
                Random.GetBytes(data);

                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
    }
}
