using Microsoft.EntityFrameworkCore;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public BaseRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected readonly Context _context;
        protected readonly DbSet<TEntity> _dbSet;

        public virtual void Delete(Guid instanceId)
        {
            var entityToDelete = _dbSet.Find(instanceId);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
            
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }



        public TEntity? GetById(Guid instanceId)
        {

            return _dbSet.Find(instanceId);     
        }

        public void Insert(TEntity newInstance)
        {
            _dbSet.Add(newInstance);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            } else
            {
                return query.ToList();
            }
        }
    }
}
