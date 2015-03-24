using WebApplication.DAL.DataContext.AccountContext;

namespace WebApplication.DAL.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        public IAccountContext Context { get; set; }

        public AccountRepository(IAccountContext context)
        {
            Context = context;
        }
    }
}
