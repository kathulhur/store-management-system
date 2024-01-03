using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Transaction;

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

        public ITransaction Reconstitute(ITransactionReconstitutionArgs transactionReconstitutionArgs)
        {

            PayLater? payLater = null;
            if (transactionReconstitutionArgs.PayLater != null)
            {
                
                payLater = new PayLater(
                    transactionReconstitutionArgs.PayLater.CustomerName,
                    transactionReconstitutionArgs.PayLater.IsPaid,
                    transactionReconstitutionArgs.PayLater.PaidAt
                );
            }

            var transactionProducts = new List<TransactionProduct>();
            foreach(var transactionProductReconstitutionArgs in transactionReconstitutionArgs.TransactionProducts)
            {
                transactionProducts.Add(new TransactionProduct(
                        transactionProductReconstitutionArgs.ProductId,
                        transactionProductReconstitutionArgs.ProductName,
                        transactionProductReconstitutionArgs.CostPrice,
                        transactionProductReconstitutionArgs.SellingPrice,
                        transactionProductReconstitutionArgs.QuantityBought
                    )
                );
            }

            return new Transaction(
                transactionReconstitutionArgs.SellerId,
                transactionReconstitutionArgs.Id,
                transactionReconstitutionArgs.DateTime,
                payLater,
                transactionProducts,
                _payLaterFactory

            );
        }
    }
}
