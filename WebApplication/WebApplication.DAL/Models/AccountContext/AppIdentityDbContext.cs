using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication.DAL.Models.AccountContext
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}
