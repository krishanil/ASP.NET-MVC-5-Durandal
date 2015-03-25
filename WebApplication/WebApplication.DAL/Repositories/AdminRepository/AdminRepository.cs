using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication.DAL.DataContext.AccountContext;
using WebApplication.DAL.Repositories.BaseRepository;

namespace WebApplication.DAL.Repositories.AdminRepository
{
    public class AdminRepository : Repository, IAdminRepository 
    {
        public AdminRepository(IAccountContext context) : base((DbContext) context) { }

        private UserManager<AppUser> userManager;

        private RoleManager<IdentityRole> roleManager;

        private UserManager<AppUser> UserManager
        {
            get
            {
                return userManager ?? new UserManager<AppUser>(new UserStore<AppUser>(Context));
            }
        }

        private RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
            }
        }
        public IQueryable<AppUser> Users()
        {
            return UserManager.Users;
        }

        public IQueryable<IdentityRole> Roles()
        {
            return RoleManager.Roles;
        }
    }
}
