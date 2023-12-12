using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");

        public TEntity? GetById(Guid instanceId);
        public void Insert(TEntity newInstance);
        public void Delete(Guid instanceId);
        public void Save();
    }
}
