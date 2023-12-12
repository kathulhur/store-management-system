    using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class CreateTransactionViewModel : ObservableObject, ICreateTransactionViewModel

    {
        public CreateTransactionViewModel(AuthContext authContext, IUnitOfWork unitOfWork, IDialogService dialogService, Action<Guid> onDone, Action onCancel)
        {
            _authContext = authContext;
            _unitOfWork = unitOfWork;
            DialogService = dialogService;
            _transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                SellerId = authContext.CurrentUser.Id
            };

            _onCancel = onCancel;
            _onDone = onDone;

            _doneCommand = new RelayCommand(OnDone, CanSubmitTransaction);
            _cancelCommand = new RelayCommand(OnCancel);

        }

        private readonly AuthContext _authContext;
        public IDialogService DialogService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Transaction _transaction;

        private readonly Action<Guid> _onDone;
        private readonly Action _onCancel;

        public ObservableCollection<ICreateTransactionProductViewModel> TransactionProducts { get; } = new ObservableCollection<ICreateTransactionProductViewModel>();

        private string _barcode = string.Empty;
        public string Barcode { get => _barcode; set => SetProperty(ref _barcode, value); }

        private string _customerName = string.Empty;
        public string CustomerName
        {
            get => _customerName;
            set
            {
                SetProperty(ref _customerName, value);
                _doneCommand.NotifyCanExecuteChanged();
            }
        }

        private bool _isPayLater;
        public bool IsPayLater
        {
            get => _isPayLater;
            set
            {
                SetProperty(ref _isPayLater, value);
                _doneCommand.NotifyCanExecuteChanged();
            }
        }

        private decimal _totalAmount;
        public decimal TotalAmount { get => _totalAmount; set => SetProperty(ref _totalAmount, value); }

        private RelayCommand _doneCommand;
        public ICommand DoneCommand { get => _doneCommand; }

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand { get => _cancelCommand; }

        public void AddProduct()
        {
            var matchedProduct = _unitOfWork.ProductRepository.GetByBarcode(Barcode);
            if (matchedProduct != null)
            {
                var transactionProduct = TransactionProducts.FirstOrDefault(e => e.ProductBarcode == matchedProduct.Barcode);
                if (transactionProduct != null)
                {
                    transactionProduct.Quantity += 1;
                }
                else
                {
                    var newTransactionProduct = new CreateTransactionProductViewModel(_unitOfWork, _transaction, matchedProduct);
                    newTransactionProduct.ItemRemoved += OnRemoveTransactionProduct;
                    newTransactionProduct.QuantityIncremented += OnTransactionProductQuantityIncrement;
                    newTransactionProduct.QuantityDecremented += OnTransactionProductQuantityDecrement;
                    TransactionProducts.Add(newTransactionProduct);
                }
                TotalAmount += matchedProduct.SellingPrice;
            }
            else
            {
                DialogService.ShowMessageDialog("Product not found", $"Product with barcode \"{Barcode}\" does not exist in the records");
            }
            Barcode = "";
            _doneCommand.NotifyCanExecuteChanged();
        }

        private bool CanSubmitTransaction()
            => TransactionProducts.Any() && (!IsPayLater || IsPayLater && CustomerName != "");

        private void OnDone()
        {
            if (_isPayLater)
            {
                var payLaterInstance = new PayLater { Id = Guid.NewGuid(), CustomerName = _customerName, TransactionId = _transaction.Id };
                _unitOfWork.PayLaterRepository.Insert(payLaterInstance);
            }

            foreach (var transactionProduct in TransactionProducts)
            {
                transactionProduct.OnDone();
            }

            _transaction.DateTime = DateTime.Now;
            _unitOfWork.TransactionRepository.Insert(_transaction);
            _unitOfWork.Save();
            _onDone(_transaction.Id);
        }

        private void UnsubscribeToTransactionProductEvents(ICreateTransactionProductViewModel item)
        {
            item.ItemRemoved -= OnRemoveTransactionProduct;
            item.QuantityIncremented -= OnTransactionProductQuantityIncrement;
            item.QuantityDecremented -= OnTransactionProductQuantityDecrement;
        }

        private void OnRemoveTransactionProduct(object? sender, EventArgs<ICreateTransactionProductViewModel> e)
        {
            UnsubscribeToTransactionProductEvents(e.Item);
            TransactionProducts.Remove(e.Item);
            TotalAmount -= e.Item.Subtotal;
            _doneCommand.NotifyCanExecuteChanged();
        }

        private void OnTransactionProductQuantityIncrement(object? sender, EventArgs<ICreateTransactionProductViewModel> e)
        {
            TotalAmount += e.Item.Price;

        }

        private void OnTransactionProductQuantityDecrement(object? sender, EventArgs<ICreateTransactionProductViewModel> e)
        {
            TotalAmount -= e.Item.Price;
        }

        private void OnCancel()
        {
            _onCancel();
        }



    }
}
