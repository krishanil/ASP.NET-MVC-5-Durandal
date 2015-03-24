using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace WebApplication.DAL.Repositories.BaseRepository
{
    public interface IRepository : IDisposable
    {
        DbContext Context { get; set; }

        IEnumerable<T> Set<T>(Expression<Func<T, bool>> lamda) where T : class;

        T Entity<T>(Expression<Func<T, bool>> lamda) where T : class;

        T Add<T>(T entity) where T : class;

        T Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        bool Save();
    }
}
