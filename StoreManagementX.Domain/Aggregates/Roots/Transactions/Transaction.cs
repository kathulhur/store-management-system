using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Transaction.TransactionProduct;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions
{
    // Aggregate


    // Aggregate root
    public class Transaction : ITransaction
    {

        internal Transaction(Guid sellerId, Guid id)
        {
            Id = id;
            SellerId = sellerId;
        }

        public Guid SellerId { get; }

        public Guid Id { get; }

        private IList<TransactionProduct> _transactionProducts = new List<TransactionProduct>();

        public IReadOnlyList<ITransactionProduct> TransactionProducts => _transactionProducts.AsReadOnly();

        public decimal TotalAmount { get; private set; }

        public void AddProduct(IProduct product)
        {
            var matchedProduct = _transactionProducts.FirstOrDefault(tp => tp.ProductId == product.Id);

            if (matchedProduct == null) // new product added
            {
                var transactionProduct = new TransactionProduct(this, product);
                _transactionProducts.Add(transactionProduct);
            } else
            {
                matchedProduct.QuantityBought += 1;
            }
            product.InStock -= 1;
        }

        public void RemoveProduct(IProduct product)
        {
            var transactionProductFound = _transactionProducts.First();
            _transactionProducts.Remove(transactionProductFound);

            TotalAmount -= transactionProductFound.TotalPrice;
        }

        public override bool Equals(object? obj)
        {
            return obj is Transaction transaction &&
                   Id.Equals(transaction.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
            => $"{nameof(Transaction)}: {Id} - Total Amount: {TotalAmount}";




        // id value object
        public class TransactionProduct : ITransactionProduct
        {
            internal TransactionProduct(Transaction transaction, IProduct product)
            {
                _transaction = transaction;
                ProductId = product.Id;

                ProductName = product.Name;
                CostPrice = product.CostPrice;
                SellingPrice = product.SellingPrice;
                QuantityBought = 1;
            }

            private Transaction _transaction;
            
            public Guid ProductId { get; }

            public string ProductName { get; }

            public decimal CostPrice { get; }

            public decimal SellingPrice { get; }

            private int _quantityBought;
            public int QuantityBought
            {
                get => _quantityBought;
                internal set
                {
                    decimal oldTotalPrice = TotalPrice;
                    _quantityBought = value;
                    decimal newTotalPrice = TotalPrice;

                    _transaction.TotalAmount = _transaction.TotalAmount - oldTotalPrice + newTotalPrice;

                }
            }


            public decimal TotalPrice => SellingPrice * QuantityBought;
            
            public class TotalPriceChangedEventArgs : EventArgs
            {
                internal TotalPriceChangedEventArgs(decimal oldTotalPrice, decimal newTotalPrice)
                {
                    OldTotalPrice = oldTotalPrice;
                    NewTotalPrice = newTotalPrice;
                }

                public decimal OldTotalPrice { get; }
                public decimal NewTotalPrice { get; }
            }

            public override bool Equals(object? obj)
            {
                return obj is TransactionProduct product &&
                       ProductId.Equals(product.ProductId);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(ProductId);
            }
        }
    }
}
