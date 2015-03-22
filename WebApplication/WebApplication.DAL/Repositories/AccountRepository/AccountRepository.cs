using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DAL.Models.AccountContext;

namespace WebApplication.DAL.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private AppIdentityDbContext context;

        public AppIdentityDbContext Context { get { return context ?? (context = new AppIdentityDbContext()); } }
    }
}
