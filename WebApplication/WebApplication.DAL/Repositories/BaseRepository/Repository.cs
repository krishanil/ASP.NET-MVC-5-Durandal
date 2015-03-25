using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplication.DAL.Repositories.BaseRepository
{
    public class Repository : IRepository
    {
        private bool disposed;

        public DbContext Context { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        #region base operations

        public DbSet<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        public IEnumerable<T> Set<T>(Expression<Func<T, bool>> lamda) where T : class
        {
            return Context.Set<T>().Where(lamda.Compile());
        }

        public T Entity<T>(Expression<Func<T, bool>> lamda) where T : class
        {
            return Context.Set<T>().SingleOrDefault(lamda.Compile());
        }

        public T Add<T>(T entity) where T : class
        {
            return Context.Set<T>().Add(entity);
        }

        public T Update<T>(T entity) where T : class
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public bool Save()
        {
            return Context.SaveChanges() > 0;
        }

        #endregion base operations

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing) Context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
