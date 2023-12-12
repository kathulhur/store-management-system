using StoreManagementSystemX.Database;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Domain.Factories.Products;
using StoreManagementSystemX.Domain.Factories.StockPurchases;
using StoreManagementSystemX.Domain.Factories.Transactions;
using StoreManagementSystemX.Domain.Factories.Users;
using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
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

            var barcodeGeneratorService = new BarcodeGeneratorService(new ProductRepository(new Context()));
            var dialogService = new DialogService();
            var unitOfWorkFactory = new UnitOfWorkFactory();

            var productFactory = new ProductFactory();
            var transactionFactory = new TransactionFactory();
            var stockPurchaseFactory = new StockPurchaseFactory();
            var userFactory = new Domain.Factories.Users.UserFactory(productFactory, transactionFactory, stockPurchaseFactory);

            IUserRepository userRepository = new Domain.Repositories.Users.UserRepository(userFactory);
            var admin = userFactory.Create(new CreateUserArgs { CreatorId = Guid.NewGuid(), Username = "admin", Password = "password" });
            userRepository.Add(admin);
            AuthenticationService = new AuthenticationService(userRepository, dialogService);
            LoginViewModel loginViewModel = new LoginViewModel(AuthenticationService, dialogService);

            NavigationService = new NavigationService(
                loginViewModel, 
                unitOfWorkFactory, 
                AuthenticationService, 
                dialogService
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
        private readonly ProductRepository _productRepository;
        private readonly TransactionRepository _transactionRepository;
        private readonly Database.DAL.UserRepository _userRepository;

    }
}
