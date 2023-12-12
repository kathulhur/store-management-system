using StoreManagementSystemX.Database;
using StoreManagementSystemX.Database.DAL;
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
            Console.Write("barcode: " + barcodeGeneratorService.GenerateBarcode());
            var dialogService = new DialogService();
            var unitOfWorkFactory = new UnitOfWorkFactory();
            AuthenticationService = new AuthenticationService(dialogService);
            LoginViewModel loginViewModel = new LoginViewModel(AuthenticationService, dialogService);
            NavigationService = new NavigationService(loginViewModel, unitOfWorkFactory, AuthenticationService, dialogService);
        }
        
        public INavigationService NavigationService { get; }
        public IAuthenticationService AuthenticationService { get; }
        private readonly ProductRepository _productRepository;
        private readonly TransactionRepository _transactionRepository;
        private readonly UserRepository _userRepository;

    }
}
