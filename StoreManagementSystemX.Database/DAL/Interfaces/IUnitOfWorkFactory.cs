using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork CreateUnitOfWork();
    }
}
