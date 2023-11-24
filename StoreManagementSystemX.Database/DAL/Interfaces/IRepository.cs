using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces
{
    public interface IRepository<TModel>
    {
        public IEnumerable<TModel> GetAll();
        public TModel? GetById(Guid instanceId);
        public void Insert(TModel newInstance);
        public void Delete(Guid instanceId);
        public void Save();
    }
}
