using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.StockPurchases.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.StockPurchases.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.StockPurchases.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.StockPurchases
{
    public class CreateStockPurchaseViewModel : BaseViewModel, ICreateStockPurchaseViewModel
    {

        public CreateStockPurchaseViewModel(
            AuthContext authContext, 
            Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository stockPurchaseRepository, 
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository, 
            IDialogService dialogService, 
            Action<Guid> onDone, 
            Action onCancel)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
            _productRepository = productRepository;
            _authContext = authContext;
            _dialogService = dialogService;
            _onDone = onDone;

            _stockPurchase = authContext.CurrentUser.StockPurchaseFactory.Create(authContext.CurrentUser.Id);
            StockPurchaseProducts = new ObservableCollection<ICreateStockPurchaseProductViewModel>();

            _cancelCommand = new RelayCommand(onCancel);
            _doneCommand = new RelayCommand(OnDone, CanSubmit);


        }

        private readonly IStockPurchase _stockPurchase;

        private readonly Action<Guid> _onDone;

        private readonly AuthContext _authContext;

        private readonly IDialogService _dialogService;

        private readonly Domain.Repositories.StockPurchases.Interfaces.IStockPurchaseRepository _stockPurchaseRepository;

        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;

        private string _barcode = string.Empty;
        public string Barcode { get => _barcode; set => SetProperty(ref _barcode, value); }

        public ObservableCollection<ICreateStockPurchaseProductViewModel> StockPurchaseProducts { get; }

        public void AddProduct()
        {
            IProduct? matchedProduct = null;
            matchedProduct = _productRepository.GetByBarcode(Barcode);

            // a product with the barcode was found
            if (matchedProduct != null)
            {
                // add the product to the actual stock purchase instance

                // check whether the view model for the stock purchase product already exists;
                //      if it is, create an instance and add to the collection
                //      otherwise: just increment the stock purchase product
                var stockPurchaseProduct = _stockPurchase.StockPurchaseProducts.FirstOrDefault(e => e.ProductId == matchedProduct.Id);
                if(stockPurchaseProduct == null)// stock purchase product doesn't exist yet
                {
                    var newStockPurchaseProduct = _stockPurchase.AddProduct(matchedProduct);
                    StockPurchaseProducts.Add(
                        new CreateStockPurchaseProductViewModel(
                            _stockPurchase,
                            matchedProduct,
                            (CreateStockPurchaseProductViewModel spp) => RemoveProductHandler(spp),
                            (CreateStockPurchaseProductViewModel spp) => IncrementProductQuantityHandler(matchedProduct),
                            (CreateStockPurchaseProductViewModel spp) => DecrementProductQuantityHandler(matchedProduct)
                        )
                    );


                } else // stock purchase product already exists
                {
                    var createStockPurchaseProductViewModel = StockPurchaseProducts.First(e => e.Barcode == stockPurchaseProduct.Barcode);
                    createStockPurchaseProductViewModel.IncrementQuantityCommand.Execute(null);
                }
                OnPropertyChanged(nameof(TotalAmount));
                _doneCommand.NotifyCanExecuteChanged();
                Barcode = "";

            }
            else
            {
                _dialogService.ShowMessageDialog("Product not found", $"Product with barcode {Barcode} does not exist in the records");
            }
        }

        public void RemoveProductHandler(ICreateStockPurchaseProductViewModel stockPurchaseProduct)
        {
            StockPurchaseProducts.Remove(stockPurchaseProduct);
            OnPropertyChanged(nameof(TotalAmount));
            _doneCommand.NotifyCanExecuteChanged();
        }

        public void IncrementProductQuantityHandler(IProduct product)
         {
            OnPropertyChanged(nameof(TotalAmount));
            _doneCommand.NotifyCanExecuteChanged();
        }

        public void DecrementProductQuantityHandler(IProduct product)
        {
            OnPropertyChanged(nameof(TotalAmount));
            _doneCommand.NotifyCanExecuteChanged();
        }

        public decimal TotalAmount
        {
            get => _stockPurchase.TotalAmount;
        }

        private readonly RelayCommand _cancelCommand;
        public ICommand CancelCommand { get => _cancelCommand; }

        private readonly RelayCommand _doneCommand;
        public ICommand DoneCommand { get => _doneCommand; }


        private bool CanSubmit()
            => StockPurchaseProducts.Any();


        public void OnDone()
        {
            _stockPurchaseRepository.Add(_stockPurchase);
            _onDone(_stockPurchase.Id);

        }
    }
}
