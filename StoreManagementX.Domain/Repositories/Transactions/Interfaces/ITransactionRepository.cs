using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces
{
    public interface ITransactionRepository : IRepository<ITransaction>
    {
    }
}
