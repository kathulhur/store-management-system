using StoreManagementSystemX.Database.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Database.DAL
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}
