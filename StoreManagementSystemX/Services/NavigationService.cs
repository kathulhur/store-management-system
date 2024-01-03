using CommunityToolkit.Mvvm.ComponentModel;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels;
using StoreManagementSystemX.ViewModels.Products;
using StoreManagementSystemX.ViewModels.StockPurchases;
using StoreManagementSystemX.ViewModels.Transactions;
using StoreManagementSystemX.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManagementSystemX.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        public NavigationService
        (
            BaseViewModel initialViewModel,
            IAuthenticationService authenticationService,
            IDialogService dialogService,
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository,
            Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository stockPurchaseRepository,
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository,
            Domain.Repositories.Users.Interfaces.IUserRepository userRepository
        )
        {
            _currentViewModel = initialViewModel;
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _productRepository = productRepository;
            _stockPurchaseRepository = stockPurchaseRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _productUpdateService = new ProductUpdateService(productRepository);
            _barcodeImageService = new BarcodeImageService();
            _productCreationService = new ProductCreationService(_productRepository, _barcodeImageService);
            _userCreationService = new UserCreationService(_userRepository);
            _stockPurchaseCreationService = new StockPurchaseCreationService(stockPurchaseRepository, productRepository, _dialogService);
            _transactionCreationService = new TransactionCreationService(_transactionRepository, _productRepository, _dialogService);

        }
        private readonly IBarcodeImageService _barcodeImageService;
        private readonly IDialogService _dialogService;
        private readonly ProductUpdateService _productUpdateService;
        private readonly ProductCreationService _productCreationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserCreationService _userCreationService;
        private readonly StockPurchaseCreationService _stockPurchaseCreationService;
        private readonly TransactionCreationService _transactionCreationService;

        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;
        private readonly Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository _stockPurchaseRepository;
        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;
        private readonly Domain.Repositories.Users.Interfaces.IUserRepository _userRepository;
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        public void NavigateTo(View view)
        {
            if (view == View.Login)
            {
                CurrentViewModel = new LoginViewModel(_authenticationService, _dialogService);
            }
            else if (view == View.Dashboard && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new DashboardViewModel(_authenticationService.AuthContext, _transactionRepository, _dialogService, _transactionCreationService);
            }
            else if (view == View.Inventory && _authenticationService.AuthContext != null)
            {

                CurrentViewModel = new InventoryViewModel(_authenticationService.AuthContext, _productRepository, _dialogService, _productUpdateService, _productCreationService, _barcodeImageService);
            }
            else if (view == View.Transactions && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new TransactionListViewModel(_authenticationService.AuthContext, _transactionRepository, _dialogService, _transactionCreationService);
            }
            else if (view == View.PayLaterTransactions && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new PayLaterTransactionsViewModel(_authenticationService.AuthContext, _transactionRepository, _dialogService, _transactionCreationService);
            }
            else if (view == View.UserList && _authenticationService.AuthContext != null)
            {

                CurrentViewModel = new UserListViewModel(_authenticationService.AuthContext, _userRepository, _dialogService, _userCreationService);
            }
            else if (view == View.StockPurchaseList && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new StockPurchaseListViewModel(_authenticationService.AuthContext, _stockPurchaseRepository, _dialogService, _stockPurchaseCreationService);
            }
            else
            {
                CurrentViewModel = new LoginViewModel(_authenticationService, _dialogService);
            }
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
