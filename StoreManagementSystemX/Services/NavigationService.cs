using CommunityToolkit.Mvvm.ComponentModel;
using StoreManagementSystemX.Database;
using StoreManagementSystemX.Database.DAL;
using StoreManagementSystemX.Database.DAL.Interfaces;
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
            IUnitOfWorkFactory unitOfWorkFactory,
            IAuthenticationService authenticationService,
            IDialogService dialogService,
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository 
        )
        {
            _currentViewModel = initialViewModel;
            _authenticationService = authenticationService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _dialogService = dialogService;
            _productRepository = productRepository;
            _productUpdateService = new ProductUpdateService(productRepository);
            _barcodeImageService = new BarcodeImageService();
            _productCreationService = new ProductCreationService(_productRepository, _barcodeImageService);
            _userCreationService = new UserCreationService(_unitOfWorkFactory);
            _stockPurchaseCreationService = new StockPurchaseCreationService(_unitOfWorkFactory, _dialogService);
            _transactionCreationService = new TransactionCreationService(_unitOfWorkFactory, _dialogService);

        }
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IBarcodeImageService _barcodeImageService;
        private readonly IDialogService _dialogService;
        private readonly ProductUpdateService _productUpdateService;
        private readonly ProductCreationService _productCreationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserCreationService _userCreationService;
        private readonly StockPurchaseCreationService _stockPurchaseCreationService;
        private readonly TransactionCreationService _transactionCreationService;
        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }
        private UnitOfWork? _unitOfWork;
        public void NavigateTo(View view)
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }

            _unitOfWork = new UnitOfWork();
            if (view == View.Login)
            {
                CurrentViewModel = new LoginViewModel(_authenticationService, _dialogService);
            }
            else if (view == View.Dashboard && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new DashboardViewModel(_authenticationService.AuthContext, _unitOfWorkFactory, _dialogService, _transactionCreationService);
            }
            else if (view == View.Inventory && _authenticationService.AuthContext != null)
            {

                CurrentViewModel = new InventoryViewModel(_authenticationService.AuthContext, _productRepository, _dialogService, _productUpdateService, _productCreationService, _barcodeImageService);
            }
            else if (view == View.Transactions && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new TransactionListViewModel(_authenticationService.AuthContext, _unitOfWorkFactory, _dialogService, _transactionCreationService);
            }
            else if (view == View.PayLaterTransactions && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new PayLaterTransactionsViewModel(_authenticationService.AuthContext, _unitOfWorkFactory, _dialogService, _transactionCreationService);
            }
            else if (view == View.UserList && _authenticationService.AuthContext != null)
            {

                CurrentViewModel = new UserListViewModel(_authenticationService.AuthContext, _unitOfWorkFactory, _dialogService, _userCreationService);
            }
            else if (view == View.StockPurchaseList && _authenticationService.AuthContext != null)
            {
                CurrentViewModel = new StockPurchaseListViewModel(_authenticationService.AuthContext, _unitOfWorkFactory, _dialogService, _stockPurchaseCreationService);
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
