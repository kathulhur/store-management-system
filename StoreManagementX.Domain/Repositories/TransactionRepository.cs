using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Repositories
{
    public class TransactionRepository : IRepository<ITransaction>
    {
        private readonly List<ITransaction> _transactions;
        public TransactionRepository()
        {
            _transactions = new List<ITransaction>();
        }

        public void Add(ITransaction newEntity)
        {
            _transactions.Add(newEntity);
        }

        public IEnumerable<ITransaction> GetAll()
        {
            return _transactions;
        }

        public ITransaction? GetById(Guid id)
        {
            return _transactions.Find(t => t.Id == id);
        }

        public void Remove(Guid id)
        {
            var transactionToRemove = _transactions.Find(t => t.Id == id);
            if (transactionToRemove != null)
            {
                _transactions.Remove(transactionToRemove);
            }
        }

        public void Update(ITransaction updatedEntity)
        {
            var transactionToUpdateIndex = _transactions.FindIndex(t => t.Id != updatedEntity.Id);
            if (transactionToUpdateIndex != -1)
            {
                _transactions[transactionToUpdateIndex] = updatedEntity;
            }
        }
    }
}
