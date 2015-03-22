using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DAL.Models.AccountContext;

namespace WebApplication.DAL.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        AppIdentityDbContext Context { get; }
    }
}
