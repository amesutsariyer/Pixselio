using Microsoft.EntityFrameworkCore;
using Pixselio.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
 

namespace Pixselio.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IdentityDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(IdentityDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
    
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }
        public T GetById(object id)
        {
            var entity = _dbSet.Find(id);

            var entty = entity as BaseEntity;
            if (entty != null && entty.IsDeleted)
            {
                return null;
            }

            return entity;
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking().FirstOrDefault();
        }

        public void Add(T entity )
        {
            _dbSet.Add(entity);

            if (entity is BaseEntity)
            {
                BaseEntity BaseEntity = entity as BaseEntity;
                BaseEntity.CreatedDate = DateTime.Now;
            }
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("IsDeleted")?.SetValue(_entity, false);
            }

        }

        public void Update(T entity)
        {
            var state = _context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            T _entity = entity;

            if (entity is BaseEntity)
            {
                BaseEntity BaseEntity = entity as BaseEntity;
                BaseEntity.UpdatedDate = DateTime.Now;
                BaseEntity.UpdatedBy ="";
            }

            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                _entity.GetType().GetProperty("IsDeleted")?.SetValue(_entity, true);

                Update(_entity);
            }
            else
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

    }
}
