using System;
using System.Data.Entity;

namespace WebApplication.DAL.Repositories.BaseRepository
{
    public class Repository : IRepository
    {
        private bool disposed;

        public DbContext Context { get; set; }

        protected Repository() { }

        public Repository(DbContext context)
        {
            Context = context;
        }

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
