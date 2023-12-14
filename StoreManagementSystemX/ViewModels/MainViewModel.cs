using StoreManagementSystemX.Database;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products;
using StoreManagementSystemX.Domain.Repositories.StockPurchases;
using StoreManagementSystemX.Domain.Repositories.Transactions;
using StoreManagementSystemX.Domain.Repositories.Users;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManagementSystemX.ViewModels
{
    public class MainViewModel: BaseViewModel
    {

        public MainViewModel()
        {
            Context dbContext = new Context();

            var dialogService = new DialogService();
            var unitOfWorkFactory = new UnitOfWorkFactory();

            var productRepository = new Domain.Repositories.Products.ProductRepository();
            var barcodeGenerationService = new BarcodeGenerationService(productRepository);
            var productFactory = new ProductFactory(barcodeGenerationService);

            var stockPurchaseRepository = new Domain.Repositories.StockPurchases.StockPurchaseRepository();

            var payLaterFactory = new PayLaterFactory();
            var transactionFactory = new TransactionFactory(payLaterFactory);
            var stockPurchaseFactory = new StockPurchaseFactory();
            var userFactory = new Domain.Factories.Users.UserFactory(productFactory, transactionFactory, stockPurchaseFactory);

            var transactionRepository = new Domain.Repositories.Transactions.TransactionRepository();

            IUserRepository userRepository = new Domain.Repositories.Users.UserRepository(userFactory);
            var admin = userFactory.Create(new CreateUserArgs { CreatorId = Guid.NewGuid(), Username = "admin", Password = "password" });
            userRepository.Add(admin);
            AuthenticationService = new AuthenticationService(userRepository, dialogService);
            LoginViewModel loginViewModel = new LoginViewModel(AuthenticationService, dialogService);

            NavigationService = new NavigationService(
                loginViewModel,
                unitOfWorkFactory,
                AuthenticationService,
                dialogService,
                productRepository,
                stockPurchaseRepository,
                transactionRepository,
                userRepository
            );
        }

        private class CreateUserArgs : ICreateUserArgs
        {
            public Guid CreatorId { get; set; }

            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public INavigationService NavigationService { get; }
        public IAuthenticationService AuthenticationService { get; }
        private readonly Database.DAL.UserRepository _userRepository;

    }
}
