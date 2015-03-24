using System;
using System.Data.Entity;

namespace WebApplication.DAL.Repositories.BaseRepository
{
    public interface IRepository : IDisposable
    {
        DbContext Context { get; set; }
    }
}
