using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication.BLL.Models.Admin;
using WebApplication.DAL.DataContext.AccountContext;
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
        public IEnumerable<UserModel> Users()
        {
            Mapper.CreateMap<AppUser, UserModel>();
            var users = Repository.Users().ToList();
            return Mapper.Map<List<AppUser>, List<UserModel>>(users);
        }
    }
}