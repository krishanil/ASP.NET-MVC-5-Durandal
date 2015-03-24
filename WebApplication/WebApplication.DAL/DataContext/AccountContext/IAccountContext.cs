using System;

namespace WebApplication.DAL.DataContext.AccountContext
{
    public interface IAccountContext : IDisposable
    {
        IAccountContext Create();
    }
}
