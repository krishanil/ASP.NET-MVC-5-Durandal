using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication.DAL.DataContext.AccountContext;
using WebApplication.DAL.Repositories.BaseRepository;

namespace WebApplication.DAL.Repositories.AdminRepository
{
    public interface IAdminRepository : IRepository
    {
        IQueryable<AppUser> Users();
        
        IQueryable<IdentityRole> Roles();
    }
}