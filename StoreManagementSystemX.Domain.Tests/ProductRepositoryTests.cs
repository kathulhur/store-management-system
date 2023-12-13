//using StoreManagementSystemX.Domain.Aggregates.Roots.Products;
//using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
//using StoreManagementSystemX.Domain.Factories.Products;
//using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
//using StoreManagementSystemX.Domain.Repositories;
//using StoreManagementSystemX.Domain.Repositories.Interfaces;
//using StoreManagementSystemX.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementSystemX.Domain.Tests
//{
//    public class ProductRepositoryTests
//    {
        
//        private IRepository<IProduct> CreateRepositoryWithSingleProduct()
//        {
//            var repository = new ProductRepository();
//            var productFactory = new ProductFactory();

//            Assert.Empty(repository.GetAll());

//            ICreateProductArgs newProductArgs = new CreateProductArgs { Name = "Product1", CostPrice = 10, SellingPrice = 20, CreatorId = Guid.NewGuid() };
//            var product = productFactory.Create(newProductArgs);
//            repository.Add(product);
//            Assert.True(repository.GetAll().Count() == 1);

//            return repository;
//        }

//        [Fact]
//        public void Product_gets_deleted_on_delete()
//        {
//            // assemble
//            var repository = CreateRepositoryWithSingleProduct();
//            var productToRemove = repository.GetAll().First();


//            //Act
//            repository.Remove(productToRemove.Id);

//            //Assert
//            Assert.Empty(repository.GetAll());
//        }

//        private IRepository<IProduct> CreateEmptyProductRepository()
//        {
//            var productRepository = new ProductRepository();
//            Assert.Empty(productRepository.GetAll());

//            return productRepository;
//        }

//        [Fact]
//        public void Product_gets_added_on_add()
//        {
//            var name = "New Product";
//            var costPrice = 10;
//            var sellingPrice = 20;
//            var inStock = 5;
//            var creatorId = Guid.NewGuid();

//            // assemble
//            var repository = CreateEmptyProductRepository();

//            var productFactory = new ProductFactory();

//            var newProduct = productFactory.Create(new CreateProductArgs { Name = name, CostPrice = costPrice, SellingPrice = sellingPrice, InStock = inStock, CreatorId = creatorId });

//            //Act
//            repository.Add(newProduct);

//            //Assert
//            var allProducts = repository.GetAll();
//            Assert.NotEmpty(allProducts);
//            Assert.True(allProducts.Count() == 1);


//            var storedProduct = allProducts.First();
//            Assert.True(storedProduct.Name == name);
//            Assert.True(storedProduct.CostPrice == costPrice);
//            Assert.True(storedProduct.SellingPrice == sellingPrice);
//            Assert.True(storedProduct.InStock == inStock);
//            Assert.True(storedProduct.CreatorId == creatorId);

//        }

//        class CreateProductArgs : ICreateProductArgs
//        {
//            public string Name { get; set; }

//            public decimal CostPrice { get; set; }

//            public decimal SellingPrice { get; set; }

//            public int InStock { get; set; }

//            public Guid CreatorId { get; set; }
//        }


//    }
//}
