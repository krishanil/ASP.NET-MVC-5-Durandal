using System.Data.Entity;
using WebApplication.DAL.Repositories.BaseRepository;

namespace WebApplication.DAL.Repositories.AdminRepository
{
    public class AdminRepository : Repository, IAdminRepository 
    {
        public AdminRepository(DbContext context) : base(context)
        {
        }
    }
}
