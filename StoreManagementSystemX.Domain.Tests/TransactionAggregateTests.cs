using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products;
using StoreManagementSystemX.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Domain.Tests
{
    public class TransactionAggregateTests
    {

        private readonly ITransactionFactory _transactionFactory = new TransactionFactory();
        private readonly IProductFactory _productFactory = new ProductFactory(new BarcodeGenerationService(new ProductRepository()));

        private ITransaction CreateEmptyTransaction()
        {

            var newTransaction = _transactionFactory.Create(sellerId: Guid.NewGuid());

            Assert.NotNull(newTransaction);

            Assert.Empty(newTransaction.TransactionProducts);

            return newTransaction;
        }

        private IProduct CreateProduct(string name, decimal costPrice, decimal sellingPrice, int inStock)
            => _productFactory.Create(new CreateProductArgs { Name = name, CostPrice = costPrice, SellingPrice = sellingPrice, InStock = inStock, CreatorId = Guid.NewGuid() });

        [Fact]
        public void Product_appears_on_transaction_upon_adding()
        {
            // arrange
            var transaction = CreateEmptyTransaction();
            var product1 = CreateProduct("product1", 10, 20, 5);
            
            // Act
            transaction.AddProduct(product1);
            var transactionProducts = transaction.TransactionProducts;


            // Assert
            Assert.NotEmpty(transactionProducts);
            Assert.Single(transactionProducts);
        }


        [Fact]
        public void Transaction_correctly_calculates_total_amount()
        {
            // arrange
            var transaction = CreateEmptyTransaction();
            var product1 = CreateProduct("product1", 10, 20, 5);
            var product2 = CreateProduct("product1", 15, 31, 5);

            // Act
            transaction.AddProduct(product1); // 20
            transaction.AddProduct(product1); // 20
            transaction.AddProduct(product2); // 31

            var transactionProducts = transaction.TransactionProducts;


            // Assert
            Assert.Equal(71, transaction.TotalAmount);
        }

        [Fact]
        public void Transaction_maintains_a_single_product_record_when_single_product_gets_added_multiple_times()
        {
            // arrange
            var transaction = CreateEmptyTransaction();
            var product1 = CreateProduct("product1", 10, 20, 5);

            // Act
            transaction.AddProduct(product1); // 20
            transaction.AddProduct(product1); // 20
            transaction.AddProduct(product1); // 20
            transaction.AddProduct(product1); // 20

            var transactionProducts = transaction.TransactionProducts;


            // Assert
            Assert.NotEmpty(transactionProducts);
            Assert.Single(transactionProducts);
            Assert.Equal(80, transaction.TotalAmount);
        }


        private class CreateProductArgs : ICreateProductArgs
        {
            public Guid CreatorId { get; set; }

            public string Name { get; set; }

            public decimal CostPrice { get; set; }

            public decimal SellingPrice { get; set; }

            public int InStock { get; set; }

            public string? Barcode { get; set; }
        }

    }
}
