using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Factories.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
        public TransactionFactory(PayLaterFactory payLaterFactory)
        {
            _payLaterFactory = payLaterFactory;
        }

        private readonly PayLaterFactory _payLaterFactory;

        public ITransaction Create(Guid sellerId)
        {
            return new Transaction(sellerId, Guid.NewGuid(), _payLaterFactory);
        }
    }
}
