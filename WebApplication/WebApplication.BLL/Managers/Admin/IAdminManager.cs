using System.Collections.Generic;
using WebApplication.BLL.Models.Admin;
using WebApplication.DAL.Repositories.AdminRepository;

namespace WebApplication.BLL.Managers.Admin
{
    public interface IAdminManager
    {
        IAdminRepository Repository { get; set; }

        IEnumerable<UserModel> Users();
    }
}