using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;

namespace StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases
{
    // Aggregate

    // Aggregate root
    public class StockPurchase : IStockPurchase
    {

        internal StockPurchase(Guid stockManagerId, Guid id)
        {
            Id = id;
            StockManagerId = stockManagerId;
            DateTime = DateTime.Now;
        }

        internal StockPurchase(Guid stockManagerId, Guid id, DateTime dateTime, decimal totalAmount, IList<StockPurchaseProduct> stockPurchaseProduct)
        {
            StockManagerId = stockManagerId;
            Id = id;
            DateTime = dateTime;
            TotalAmount = totalAmount;
            _stockPurchaseProduct = stockPurchaseProduct;
        }

        public Guid Id { get; }

        private IList<StockPurchaseProduct> _stockPurchaseProduct = new List<StockPurchaseProduct>();

        public IReadOnlyList<IStockPurchaseProduct> StockPurchaseProducts => _stockPurchaseProduct.AsReadOnly();

        public decimal TotalAmount { get; private set; }

        public Guid StockManagerId { get; }

        public DateTime DateTime { get; }

        // Add single quantity of the product
        public IStockPurchaseProduct AddProduct(IProduct product)
        {
            var matchedProduct = _stockPurchaseProduct.FirstOrDefault(tp => tp.ProductId == product.Id);

            if (matchedProduct == null) // new product added
            {
                var stockPurchaseProduct = new StockPurchaseProduct(product);
                _stockPurchaseProduct.Add(stockPurchaseProduct);
                product.InStock += 1;
                TotalAmount += product.CostPrice;
                return stockPurchaseProduct;
            } else
            {
                TotalAmount += product.CostPrice;
                matchedProduct.QuantityBought += 1;
                return matchedProduct;
            }
        }


        public IStockPurchaseProduct IncrementProduct(IProduct product, int quantity = 1)
        {
            var stockPurchaseProduct = _stockPurchaseProduct.First(tp => tp.ProductId == product.Id);
            stockPurchaseProduct.QuantityBought += quantity;
            TotalAmount += product.CostPrice * quantity;
            return stockPurchaseProduct;
        }

        public IStockPurchaseProduct DecrementProduct(IProduct product, int quantity = 1)
        {
            var stockPurchaseProduct = _stockPurchaseProduct.First(tp => tp.ProductId == product.Id);
            stockPurchaseProduct.QuantityBought -= quantity;
            TotalAmount -= product.CostPrice * quantity;
            return stockPurchaseProduct;
        }

        // Remove all of this product in the stock purchase
        public IStockPurchaseProduct RemoveProduct(IProduct product)
        {
            var stockPurchaseProductFound = _stockPurchaseProduct.First(e => e.ProductId == product.Id);
            product.InStock -= stockPurchaseProductFound.QuantityBought;
            _stockPurchaseProduct.Remove(stockPurchaseProductFound);
            TotalAmount -= stockPurchaseProductFound.TotalCost;

            return stockPurchaseProductFound;
        }

        public override bool Equals(object? obj)
        {
            return obj is StockPurchase stockPurchase &&
                   Id.Equals(stockPurchase.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
            => $"{nameof(StockPurchase)}: {Id} - Total Amount: {TotalAmount}";





        // id value object
        public class StockPurchaseProduct : IStockPurchaseProduct
        {
            internal StockPurchaseProduct(IProduct product)
            {
                ProductId = product.Id;
                Name = product.Name;
                Price = product.CostPrice;
                Barcode = product.Barcode;
                QuantityBought = 1;
            }

            internal StockPurchaseProduct (Guid productId, string barcode, string name, decimal price, int quantityBought)
            {
                ProductId = productId;
                Name = name;
                Price = price;
                QuantityBought = quantityBought;
                Barcode = barcode;
            }

            public Guid ProductId { get; }

            public string Name { get; }

            public decimal Price { get; }

            public int QuantityBought { get; internal set; }

            public decimal TotalCost => Price * QuantityBought;

            public string Barcode { get; }

            public override bool Equals(object? obj)
            {
                return obj is StockPurchaseProduct stockPurchaseProduct &&
                       ProductId.Equals(stockPurchaseProduct.ProductId);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(ProductId);
            }
        }
    }
}
