using WebApplication.DAL.DataContext.AccountContext;

namespace WebApplication.DAL.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
         IAccountContext Context { get; set; }
    }
}
