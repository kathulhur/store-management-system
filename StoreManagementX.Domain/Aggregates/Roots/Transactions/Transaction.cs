using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Transaction.TransactionProduct;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.Transactions
{
    // Aggregate


    // Aggregate root
    public class Transaction : ITransaction
    {

        internal Transaction(Guid sellerId, Guid id, PayLaterFactory payLaterFactory)
        {
            Id = id;
            SellerId = sellerId;
            DateTime = DateTime.Now;
            _payLaterFactory = payLaterFactory;
            _transactionProducts = new List<TransactionProduct>();
        }

        internal Transaction(Guid sellerId, Guid id, DateTime dateTime, PayLater? payLater, List<TransactionProduct> transactionProducts, PayLaterFactory payLaterFactory)
        {
            SellerId = sellerId;
            Id = id;
            DateTime = dateTime;
            _payLater = payLater;
            _transactionProducts = transactionProducts;
            _payLaterFactory = payLaterFactory;
        }

        private readonly PayLaterFactory _payLaterFactory;

        public Guid SellerId { get; }

        public Guid Id { get; }

        private IList<TransactionProduct> _transactionProducts;

        public IReadOnlyList<ITransactionProduct> TransactionProducts => _transactionProducts.AsReadOnly();

        public decimal TotalAmount { get; private set; }

        private PayLater? _payLater { get; set; }

        public IPayLater? PayLater => _payLater;

        public DateTime DateTime { get; }

        public ITransactionProduct AddProduct(IProduct product)
        {
            var matchedProduct = _transactionProducts.FirstOrDefault(tp => tp.ProductId == product.Id);

            if (matchedProduct == null) // new product added
            {
                var transactionProduct = new TransactionProduct(product);
                _transactionProducts.Add(transactionProduct);
                product.InStock -= 1;
                TotalAmount += product.SellingPrice;
                return transactionProduct;
            } else
            {
                return IncrementProduct(product);
            }
        }

        public ITransactionProduct IncrementProduct(IProduct product, int quantity = 1)
        {
            var transactionProduct = _transactionProducts.First(tp => tp.ProductId == product.Id);

            transactionProduct.QuantityBought += quantity;
            product.InStock -= quantity;

            TotalAmount += product.SellingPrice * quantity;

            return transactionProduct;
        }

        public ITransactionProduct DecrementProduct(IProduct product, int quantity = 1)
        {
            var transactionProduct = _transactionProducts.First(tp => tp.ProductId == product.Id);

            if (quantity < 0 || quantity > transactionProduct.QuantityBought)
                throw new ArgumentOutOfRangeException("Invalid Quantity argument");

            transactionProduct.QuantityBought -= quantity;
            product.InStock += quantity;

            TotalAmount -= product.SellingPrice * quantity;

            return transactionProduct;
        }

        public ITransactionProduct RemoveProduct(IProduct product)
        {
            var transactionProductFound = _transactionProducts.First();
            product.InStock += transactionProductFound.QuantityBought;
            _transactionProducts.Remove(transactionProductFound);

            TotalAmount -= transactionProductFound.TotalPrice;

            return transactionProductFound;
        }

        public void MarkAsPaid()
        {
            if(_payLater != null)
            {
                _payLater.IsPaid = true;
                _payLater.PaidAt = DateTime.Now;
            }
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


        public void SetPayLaterDetails(string customerName)
        {
            _payLater = (PayLater) _payLaterFactory.Create(customerName);
        }


        public class TransactionProduct : ITransactionProduct
        {
            internal TransactionProduct(IProduct product)
            {
                ProductId = product.Id;
                ProductName = product.Name;
                CostPrice = product.CostPrice;
                SellingPrice = product.SellingPrice;
                QuantityBought = 1;
            }

            internal TransactionProduct(Guid productId, string productName, decimal costPrice, decimal sellingPrice, int quantityBought)
            {
                ProductId = productId;
                ProductName = productName;
                CostPrice = costPrice;
                SellingPrice = sellingPrice;
                QuantityBought = quantityBought;
            }

            public Guid ProductId { get; }

            public string ProductName { get; }

            public decimal CostPrice { get; }

            public decimal SellingPrice { get; }

            public int QuantityBought { get; internal set; }


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
