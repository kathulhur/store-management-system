using NSubstitute;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.Products.Interfaces;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories;
using StoreManagementSystemX.Domain.Repositories.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products;
using StoreManagementSystemX.Domain.Repositories.Users;
using StoreManagementSystemX.Domain.Services.Barcode.Interfaces;
using StoreManagementSystemX.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Tests
{
    public class UserRepositoryTests
    {
        private UserFactory CreateUserFactory()
        {
            var productRepository = new ProductRepository();
            var barcodeGenerationService = new BarcodeGenerationService(productRepository);
            var productFactory = new ProductFactory(barcodeGenerationService);
            var transactionFactory = new TransactionFactory();
            var stockPurchaseFactory = new StockPurchaseFactory();
            var userFactory = new UserFactory(productFactory, transactionFactory, stockPurchaseFactory);
            return userFactory;
        }

        private IRepository<IUser> CreateRepositoryWithSingleUser()
        {
            // assemble
            var userFactory = CreateUserFactory();
            var user = userFactory.Create(new CreateUserArgs { Username = "hello", Password = "world" });

            IRepository<IUser> repository = new UserRepository(userFactory);

            // Verify empty
            Assert.False(repository.GetAll().Any());

            // act
            repository.Add(user);

            // assert
            Assert.True(repository.GetAll().Any());

            return repository;
        }

        private IRepository<IUser> CreateRepositoryWithSingleUserHavingId()
        {
            // assemble
            var userFactory = CreateUserFactory();
            var user = userFactory.Create(new CreateUserArgs { Username = "hello", Password = "world" });

            IRepository<IUser> repository = new UserRepository(userFactory);

            // Verify empty
            Assert.False(repository.GetAll().Any());

            // act
            repository.Add(user);

            // assert
            Assert.True(repository.GetAll().Any());

            return repository;
        }


        [Fact]
        public void User_gets_added_and_has_correct_username_value()
        {
            // assemble
            var userFactory = CreateUserFactory();
            IRepository<IUser> repository = new UserRepository(userFactory);

            var newUser = new CreateUserArgs { Username = "hello", Password = "world" };

            Assert.False(repository.GetAll().Any());

            // act
            repository.Add(userFactory.Create(newUser));

            // assert
            Assert.True(repository.GetAll().Any());
            var storedUser = repository.GetAll().First();
            Assert.True(storedUser.Username == newUser.Username);
        }

        [Fact]
        public void User_gets_removed_on_remove()
        {
            // assemble
            var userRepository = CreateRepositoryWithSingleUser();
            var userToDelete = userRepository.GetAll().First();


            // act
            userRepository.Remove(userToDelete.Id);

            // Assert
            Assert.True(!userRepository.GetAll().Any());
        }


        private class CreateUserArgs : ICreateUserArgs
        {
            public Guid CreatorId { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }
        }
    }
}
