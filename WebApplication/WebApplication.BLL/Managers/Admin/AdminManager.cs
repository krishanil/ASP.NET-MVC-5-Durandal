using WebApplication.DAL.Repositories.AdminRepository;

namespace WebApplication.BLL.Managers.Admin
{
    public class AdminManager : IAdminManager
    {
        public IAdminRepository Repository { get; set; }

        public AdminManager(IAdminRepository repository)
        {
            Repository = repository;
        }
    }
}