using WebApplication.DAL.Repositories.AdminRepository;

namespace WebApplication.BLL.Managers.Admin
{
    public interface IAdminManager
    {
        IAdminRepository Repository { get; set; }
    }
}