using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces
{
    // Aggregate

    // Aggregate root
    public interface ITransaction
    {
        public Guid SellerId { get; }

        public DateTime DateTime { get; }

        public Guid Id { get; }

        public IPayLater? PayLater { get; }

        public IReadOnlyList<ITransactionProduct> TransactionProducts { get; }

        public decimal TotalAmount { get; }

        public void SetPayLaterDetails(string customerName);

        public void MarkAsPaid();

        public ITransactionProduct AddProduct(IProduct product);

        public ITransactionProduct IncrementProduct(IProduct product, int quantity = 1);

        public ITransactionProduct DecrementProduct(IProduct product, int quantity = 1);

        public ITransactionProduct RemoveProduct(IProduct product);


        
        
    }
}
