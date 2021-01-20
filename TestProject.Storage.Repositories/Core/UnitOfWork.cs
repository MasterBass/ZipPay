using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Common.Attribute;
using TestProject.Storage.DAL;

namespace TestProject.Storage.Repositories.Core
{
    [ExposeForDI]
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private bool _disposed = false;
        private readonly AppDbContext _context;

        #endregion


        #region Ctor

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}