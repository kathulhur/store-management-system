using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
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

        public CreateStockPurchaseViewModel(AuthContext authContext, IUnitOfWork unitOfWork, IDialogService dialogService, Action<Guid> onDone, Action onCancel)
        {
            _unitOfWork = unitOfWork;
            _authContext = authContext;
            _dialogService = dialogService;
            _onDone = onDone;
            _stockPurchase = new StockPurchase
            {
                Id = Guid.NewGuid(),
                MadeById = authContext.CurrentUser.Id,
            };
            StockPurchaseProducts = new ObservableCollection<ICreateStockPurchaseProductViewModel>();

            _cancelCommand = new RelayCommand(onCancel);
            _doneCommand = new RelayCommand(OnDone, CanSubmit);


        }

        private readonly StockPurchase _stockPurchase;

        private readonly Action<Guid> _onDone;

        private readonly AuthContext _authContext;
        private readonly IDialogService _dialogService;

        private readonly IUnitOfWork _unitOfWork;

        private string _barcode = string.Empty;
        public string Barcode { get => _barcode; set => SetProperty(ref _barcode, value); }

        public ObservableCollection<ICreateStockPurchaseProductViewModel> StockPurchaseProducts { get; }

        public void AddProduct()
        {
            Product? matchedProduct = null;
            matchedProduct = _unitOfWork.ProductRepository.GetByBarcode(Barcode);

            if (matchedProduct != null)
            {
                var stockPurchaseProduct = StockPurchaseProducts.FirstOrDefault(p => p.ProductBarcode == Barcode);
                if (stockPurchaseProduct == null)
                {
                    var newStockPurchaseProduct = new CreateStockPurchaseProductViewModel(_stockPurchase, matchedProduct, RemoveStockPurchaseProduct, (stockPurchaseProduct) =>
                    {
                        TotalAmount += stockPurchaseProduct.Price;
                    }, (stockPurchaseProduct) =>
                    {
                        TotalAmount -= stockPurchaseProduct.Price;
                    });
                    StockPurchaseProducts.Add(newStockPurchaseProduct);
                }
                else
                {
                    stockPurchaseProduct.Quantity += 1;
                }
                TotalAmount += matchedProduct.CostPrice;
                _doneCommand.NotifyCanExecuteChanged();
            }
            else
            {
                _dialogService.ShowMessageDialog("Product not found", $"Product with barcode {Barcode} does not exist in the records");
            }
            Barcode = "";
        }

        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set => SetProperty(ref _totalAmount, value);
        }

        private readonly RelayCommand _cancelCommand;
        public ICommand CancelCommand { get => _cancelCommand; }

        private readonly RelayCommand _doneCommand;
        public ICommand DoneCommand { get => _doneCommand; }


        private bool CanSubmit()
            => StockPurchaseProducts.Any();

        private void RemoveStockPurchaseProduct(ICreateStockPurchaseProductViewModel stockPurchaseProduct)
        {
            StockPurchaseProducts.Remove(stockPurchaseProduct);
            TotalAmount -= stockPurchaseProduct.Subtotal;
            _doneCommand.NotifyCanExecuteChanged();
        }

        public void OnDone()
        {
            _stockPurchase.DateTime = DateTime.Now;
            _unitOfWork.StockPurchaseRepository.Insert(_stockPurchase);
            foreach(var stockPurchaseProduct in StockPurchaseProducts)
            {
                var newStockPurchaseProduct = stockPurchaseProduct.BuildStockPurchaseProduct();
                _unitOfWork.StockPurchaseProductRepository.Insert(newStockPurchaseProduct);
            }
            _unitOfWork.Save();
            _onDone(_stockPurchase.Id);

        }
    }
}
