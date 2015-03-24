using System.Data.Entity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication.DAL.DataContext.AccountContext
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            var adminRole = new IdentityRole { Name = "Administrators" };
            var userRole = new IdentityRole { Name = "RegisteredUsers" };

            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var hash = Crypto.HashPassword("Administrator451");

            var adminUser = new AppUser { UserName = "Administrator", Email = "Administrator@acme.com", SecurityStamp = "dummyStamp", PasswordHash = hash };

            context.Users.Add(adminUser);

            context.SaveChanges();

            adminUser.Roles.Add(new IdentityUserRole { UserId = adminUser.Id, RoleId = adminRole.Id  });
            adminUser.Roles.Add(new IdentityUserRole { UserId = adminUser.Id, RoleId = userRole.Id });
        }
    }
}
