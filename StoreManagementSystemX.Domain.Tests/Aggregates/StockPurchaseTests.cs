
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Tests.Aggregates
{
    public class StockPurchaseTests
    {

        private readonly IStockPurchaseFactory _stockPurchaseFactory = new StockPurchaseFactory();
        private readonly IProductFactory _productFactory = new ProductFactory();

        private IStockPurchase CreateEmptyStockPurchase()
        {

            var newStockPurchase = _stockPurchaseFactory.Create(stockManagerId: Guid.NewGuid());

            Assert.NotNull(newStockPurchase);

            Assert.Empty(newStockPurchase.StockPurchaseProducts);

            return newStockPurchase;
        }

        private IProduct CreateProduct(string name, decimal costPrice, decimal sellingPrice, int inStock)
            => _productFactory.Create(new CreateProductArgs { Name = name, CostPrice = costPrice, SellingPrice = sellingPrice, InStock = inStock, CreatorId = Guid.NewGuid() });

        [Fact]
        public void Product_appears_on_transaction_upon_adding()
        {
            // arrange
            var stockPurchase = CreateEmptyStockPurchase();
            var product1 = CreateProduct("product1", 10, 20, 5);

            // Act
            stockPurchase.AddProduct(product1);
            var stockPurchaseProducts = stockPurchase.StockPurchaseProducts;


            // Assert
            Assert.NotEmpty(stockPurchaseProducts);
            Assert.Single(stockPurchaseProducts);
        }


        [Fact]
        public void StockPurchase_correctly_calculates_total_amount()
        {
            // arrange
            var transaction = CreateEmptyStockPurchase();
            var product1 = CreateProduct("product1", 10, 20, 5);
            var product2 = CreateProduct("product1", 15, 31, 5);

            // Act
            transaction.AddProduct(product1); // 10
            transaction.AddProduct(product1); // 10
            transaction.AddProduct(product2); // 15

            var transactionProducts = transaction.StockPurchaseProducts;


            // Assert
            Assert.Equal(35, transaction.TotalAmount);
        }

        [Fact]
        public void StockPurchase_maintains_a_single_product_record_when_single_product_gets_added_multiple_times()
        {
            // arrange
            var stockPurchase = CreateEmptyStockPurchase();
            var product1 = CreateProduct("product1", 10, 20, 5);

            // Act
            stockPurchase.AddProduct(product1); // 10
            stockPurchase.AddProduct(product1); // 10
            stockPurchase.AddProduct(product1); // 10
            stockPurchase.AddProduct(product1); // 10

            var stockPurchaseProducts = stockPurchase.StockPurchaseProducts;


            // Assert
            Assert.NotEmpty(stockPurchaseProducts);
            Assert.Single(stockPurchaseProducts);
            Assert.Equal(40, stockPurchase.TotalAmount);
        }


        private class CreateProductArgs : ICreateProductArgs
        {
            public Guid CreatorId { get; set; }

            public string Name { get; set; }

            public decimal CostPrice { get; set; }

            public decimal SellingPrice { get; set; }

            public int InStock { get; set; }
        }

    }
}
