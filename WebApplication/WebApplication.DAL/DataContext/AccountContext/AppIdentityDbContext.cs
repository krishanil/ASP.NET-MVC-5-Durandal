using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication.DAL.DataContext.AccountContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>, IAccountContext
    {
        public AppIdentityDbContext() : base("DB", throwIfV1Schema: false)
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public IAccountContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}
