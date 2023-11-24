using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.ViewModels.Transactions.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {

        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate);

        public void Attach(Transaction transaction);
    }
}
