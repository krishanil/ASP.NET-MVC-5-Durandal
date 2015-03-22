using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;

namespace WebApplication.BLL.Managers.Account
{
    public interface IAppUserManager
    {
        void SetOwinIdentityDbContext(IAppBuilder app);
    }
}
