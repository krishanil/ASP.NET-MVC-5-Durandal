using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using WebApplication.BLL.Managers.Account;

namespace WebApplication.BLL.Providers
{
    public static class AuthenticationPropertiesConfig
    {
        public static AuthenticationProperties CreateProperties(ClaimsIdentity identity)
        {
            var roleClaimValues = identity.FindAll(ClaimTypes.Role).Select(c => c.Value);

            var roles = string.Join(",", roleClaimValues);

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", identity.FindFirstValue(ClaimTypes.Name) },
                { "userRoles", roles }
            };

            return new AuthenticationProperties(data);
        }

        public static async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context, AppUserManager userManager, string cookie)
        {
            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);

            var cookiesIdentity =
                await user.GenerateUserIdentityAsync(userManager, cookie);

            var properties = CreateProperties(oAuthIdentity);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }
    }


}
