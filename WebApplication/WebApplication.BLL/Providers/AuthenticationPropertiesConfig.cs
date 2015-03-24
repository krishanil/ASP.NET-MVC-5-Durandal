using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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
    }
}
