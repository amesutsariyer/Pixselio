using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Pixselio.Data
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IdentityDbContext _context;

        private bool _disposed;
        public UnitofWork(IdentityDbContext context)
        {
            _context = context ?? throw new ArgumentException("context is null");
        }

        public int SaveChanges()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var affectedRow = 0;
                try
                {
                    foreach (var dbEntityEntry in _context.ChangeTracker.Entries<Entity.BaseEntity>().Where(x => x.State == EntityState.Modified).ToList())
                    {
                        try
                        {
                            dbEntityEntry.Property<DateTime>("CreatedDate").IsModified = false;
                            dbEntityEntry.Property<int>("CreatedUserId").IsModified = false;
                        }
                        catch (Exception ex)
                        {
                            //Ignored
                        }

                    }

                    affectedRow = _context.SaveChanges();
                    transaction.Commit();
                    return affectedRow;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return -1;
                }
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
    }
}
