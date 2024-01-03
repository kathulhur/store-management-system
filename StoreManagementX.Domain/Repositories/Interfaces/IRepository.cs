using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories.Interfaces
{
    // Must create an illusion of an in-memory collection of all objects of a type
    public interface IRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity newEntity);

        public void Update(TEntity newEntity);

        public void Remove(Guid id);

        public TEntity? GetById(Guid id);

        public IEnumerable<TEntity> GetAll();

        
    }
}
