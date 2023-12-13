//using Microsoft.EntityFrameworkCore.Query.Internal;
//using NSubstitute;
//using NSubstitute.Extensions;
//using StoreManagementSystemX.Database.DAL.Interfaces;
//using StoreManagementSystemX.Database.Models;
//using StoreManagementSystemX.Domain.Services.Barcode.Interfaces;
//using StoreManagementSystemX.Services;
//using StoreManagementSystemX.Services.Interfaces;
//using StoreManagementSystemX.ViewModels.Products;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementSystemX.Tests
//{

//    public class InventoryViewModelTests
//    {
//        private static IBarcodeImageService _barcodeImageServce = new BarcodeImageService();
//        [Fact]
//        public void Products_loads_all_records()
//        {
//            // arrange
//            var products = new List<Product>()
//            {
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();
//            unitOfWork.ProductRepository.Get().Returns(products);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();

            
//            var authContext = new AuthContext(new User());
//            var productUpdateService = Substitute.For<IProductUpdateService>();
//            var productCreationService = Substitute.For<IProductCreationService>();
//            // act
//            var inventoryViewModel = new InventoryViewModel(
//                authContext,
//                unitOfWorkFactory, 
//                dialogService,
//                productUpdateService,
//                productCreationService,
//                _barcodeImageServce
//            );

//            // assert
//            Assert.Equal(6, inventoryViewModel.Products.Count);
//        }

//        [Fact]
//        public void Product_gets_removed_on_confirmed_deletion()
//        {
//            // arrange
//            var productId = Guid.NewGuid();
//            var productToBeDeleted = new Product { Id = productId };
//            var products = new List<Product>()
//            {
//                productToBeDeleted,
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();

//            unitOfWork.ProductRepository.Get().Returns(products);
    

//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();
//            dialogService.ShowConfirmationDialog("title", "message").ReturnsForAnyArgs(true);

//            var authContext = new AuthContext(new User());
            
//            var productUpdateService = Substitute.For<IProductUpdateService>();
            
//            var productCreationService = Substitute.For<IProductCreationService>();

//            var inventoryViewModel = new InventoryViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                productUpdateService,
//                productCreationService,
//                _barcodeImageServce
//            );

//            var foundProduct = inventoryViewModel.Products.First(p => p.Id == productId);

//            // act
//            foundProduct.DeleteCommand.Execute(null);

//            // assert
//            // products count updated
//            Assert.Equal(5, inventoryViewModel.Products.Count);

//            // product that needs to be deleted is indeed deleted
//            Assert.Null(inventoryViewModel.Products.FirstOrDefault(p => p.Id == foundProduct.Id));
//        }

//        [Fact]
//        public void Product_does_get_removed_on_canceled_deletion()
//        {
//            // arrange
//            var productId = Guid.NewGuid();
//            var productToBeDeleted = new Product { Id = productId };
//            var products = new List<Product>()
//            {
//                productToBeDeleted,
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//                new Product(),
//            };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();

//            unitOfWork.ProductRepository.Get().Returns(products);


//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();
//            dialogService.ShowConfirmationDialog("title", "message").ReturnsForAnyArgs(false);

//            var authContext = new AuthContext(new User());

//            var productUpdateService = Substitute.For<IProductUpdateService>();

//            var productCreationService = Substitute.For<IProductCreationService>();

//            var inventoryViewModel = new InventoryViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                productUpdateService,
//                productCreationService,
//                _barcodeImageServce
//            );

//            var foundProduct = inventoryViewModel.Products.First(p => p.Id == productId);

//            // act
//            foundProduct.DeleteCommand.Execute(null);

//            // assert
//            // products count updated
//            Assert.Equal(6, inventoryViewModel.Products.Count);

//            // product that needs to be deleted is indeed deleted
//            Assert.NotNull(inventoryViewModel.Products.FirstOrDefault(p => p.Id == foundProduct.Id));
//        }

//        [Fact]
//        public void Product_does_get_updated_on_update_success()
//        {
//            // arrange
//            var productId = Guid.NewGuid();
//            var productToBeUpdated = new Product { Id = productId, Name = "hello", CostPrice = 111, SellingPrice = 222 };
//            var updatedProduct = new Product { Id = productId, Name = "hello", CostPrice = 111, SellingPrice = 222 };
//            var products = new List<Product>()
//            {
//                productToBeUpdated
//            };
//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();

//            unitOfWork.ProductRepository.Get().Returns(products);
//            unitOfWork.ProductRepository.GetById(productId).Returns(updatedProduct);
//            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);

//            var dialogService = Substitute.For<IDialogService>();
//            dialogService.ShowConfirmationDialog("title", "message").ReturnsForAnyArgs(false);

//            var authContext = new AuthContext(new User());

//            var productUpdateService = Substitute.For<IProductUpdateService>();
//            productUpdateService.UpdateProduct(productId).ReturnsForAnyArgs(ProductUpdateServiceResponse.Success);

//            var productCreationService = Substitute.For<IProductCreationService>();

//            var inventoryViewModel = new InventoryViewModel(
//                authContext,
//                unitOfWorkFactory,
//                dialogService,
//                productUpdateService,
//                productCreationService,
//                _barcodeImageServce
//            );

//            var foundProduct = inventoryViewModel.Products.First(p => p.Id == productId);

//            // assert
//            // product properties must not be updated yet
//            Assert.Equal(updatedProduct.Id, foundProduct.Id);
//            Assert.Equal(updatedProduct.Name, foundProduct.Name);
//            Assert.Equal(updatedProduct.CostPrice, foundProduct.CostPrice);
//            Assert.Equal(updatedProduct.SellingPrice, foundProduct.SellingPrice);

//            // act
//            foundProduct.UpdateCommand.Execute(null);



//            // assert
//            // product properties must be updated
//            Assert.Equal(updatedProduct.Id, foundProduct.Id);
//            Assert.Equal(updatedProduct.Name, foundProduct.Name);
//            Assert.Equal(updatedProduct.CostPrice, foundProduct.CostPrice);
//            Assert.Equal(updatedProduct.SellingPrice, foundProduct.SellingPrice);

//            // product that needs to be deleted is indeed deleted
//            Assert.NotNull(inventoryViewModel.Products.FirstOrDefault(p => p.Id == foundProduct.Id));
//        }


//    }
//}
