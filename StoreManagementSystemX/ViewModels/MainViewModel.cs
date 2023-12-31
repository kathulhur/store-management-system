﻿using StoreManagementSystemX.Domain.Factories.Products;
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
using StoreManagementSystemX.Infrastructure.Persistence;
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

            var dialogService = new DialogService();

            var barcodeGenerationService = new BarcodeGenerationService();
            var productFactory = new ProductFactory(barcodeGenerationService);
            var productRepository = new ProductRepositoryImpl(productFactory);


            var payLaterFactory = new PayLaterFactory();
            var transactionFactory = new TransactionFactory(payLaterFactory);
            var stockPurchaseFactory = new StockPurchaseFactory();
            var stockPurchaseRepository = new StockPurchaseRepositoryImpl(stockPurchaseFactory);
            var userFactory = new Domain.Factories.Users.UserFactory(productFactory, transactionFactory, stockPurchaseFactory);

            var transactionRepository = new TransactionRepositoryImpl(transactionFactory);

            IUserRepository userRepository = new UserRepositoryImpl(userFactory);

            AuthenticationService = new AuthenticationService(userRepository, dialogService);
            LoginViewModel loginViewModel = new LoginViewModel(AuthenticationService, dialogService);

            NavigationService = new NavigationService(
                loginViewModel,
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

    }
}
