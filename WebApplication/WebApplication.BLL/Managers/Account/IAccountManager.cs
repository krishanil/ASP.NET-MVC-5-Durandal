using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Owin;
using WebApplication.BLL.Models.BindingModels;
using WebApplication.BLL.Models.ViewModels;

namespace WebApplication.BLL.Managers.Account
{
    public interface IAccountManager
    {
        UrlHelper UrlManager { get; set; }
        
        string StartupPublicClientId { get; set; }

        AppUserManager UserManager { get; set; }

        IAuthenticationManager AuthenticationManager { get; set; }

        void SetOwinContext(IAppBuilder app);

        UserInfoModel GetUserInfo(IPrincipal user);

        Task<ManageInfoModel> GetManageInfo(IPrincipal user, string returnUrl, bool generateState = false);

        IEnumerable<ExternalLoginModel> GetExternalLogins(string returnUrl, bool generateState = false);

        void SignOut(string authenticationType);

        Task<IdentityResult> ChangePasswordAsync(IPrincipal user, ChangePasswordModel model);

        Task<IdentityResult> AddPasswordAsync(IPrincipal user, SetPasswordModel model);

        Task<IdentityResult> AddExternalLogin(IPrincipal user, AddExternalLoginModel model, AuthenticationTicket ticket);

        Task<IdentityResult> RemoveLogin(IPrincipal user, RemoveLoginModel model);

        void SetExternalLogin(ExternalLoginData externalLogin, string authenticationType, string cookies);

        Task<IdentityResult> CreateAsync(RegisterModel model);

        Task<IdentityResult> RegisterExternal(RegisterExternalModel model);

        void UserManagerDispose(bool disposing);
    }
}
