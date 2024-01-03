    using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
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
        public CreateTransactionViewModel(
            AuthContext authContext, 
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository,
            Domain.Repositories.Products.Interfaces.IProductRepository productRepository,
            IDialogService dialogService, 
            Action<Guid> onDone, 
            Action onClose)
        {
            _authContext = authContext;
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
            DialogService = dialogService;
            _transaction = authContext.CurrentUser.TransactionFactory.Create(authContext.CurrentUser.Id);

            _onClose = onClose;
            _onDone = onDone;

            _doneCommand = new RelayCommand(OnDone, CanSubmitTransaction);
            _cancelCommand = new RelayCommand(OnCancel);

        }

        private readonly AuthContext _authContext;
        public IDialogService DialogService;

        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;
        private readonly Domain.Repositories.Products.Interfaces.IProductRepository _productRepository;

        private readonly ITransaction _transaction;

        private readonly Action<Guid> _onDone;
        private readonly Action _onClose;

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

        public decimal TotalAmount => _transaction.TotalAmount;

        private RelayCommand _doneCommand;
        public ICommand DoneCommand { get => _doneCommand; }

        private RelayCommand _cancelCommand;
        public ICommand CancelCommand { get => _cancelCommand; }

        public void AddProduct()
        {
            var matchedProduct = _productRepository.GetByBarcode(Barcode);
            if (matchedProduct != null)
            {
                // check whether the product already exists in the transaction
                var transactionProduct = _transaction.TransactionProducts.FirstOrDefault(e => e.ProductId == matchedProduct.Id);
                if (transactionProduct == null) // product does not exist yet
                {
                    AddProduct(matchedProduct);
                }
                else // product already exists
                {
                    IncrementTransactionProduct(Barcode);
                }

                OnPropertyChanged(nameof(TotalAmount));
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
            if (IsPayLater)
            {
                _transaction.SetPayLaterDetails(CustomerName);
            }
            _transactionRepository.Add(_transaction);
            _onDone(_transaction.Id);
            _onClose();
        }

        private void IncrementTransactionProduct(string barcode)
        {
            var transactionProductVM = TransactionProducts.First(e => e.ProductBarcode == barcode);
            transactionProductVM.IncrementQuantityCommand.Execute(null);

        }

        private void AddProduct(IProduct product)
        {
            var newTransactionProduct = _transaction.AddProduct(product);
            var newTransactionProductViewModel = new CreateTransactionProductViewModel(_transactionRepository, _transaction, product);
            SubscribeToTransactionProductEvents(newTransactionProductViewModel);
            TransactionProducts.Add(newTransactionProductViewModel);
        }

        private void SubscribeToTransactionProductEvents(ICreateTransactionProductViewModel item)
        {
            item.ItemRemoved += OnRemoveTransactionProduct;
            item.QuantityIncremented += OnTransactionProductQuantityIncrement;
            item.QuantityDecremented += OnTransactionProductQuantityDecrement;
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
            OnPropertyChanged(nameof(TotalAmount));
            _doneCommand.NotifyCanExecuteChanged();
        }

        private void OnTransactionProductQuantityIncrement(object? sender, EventArgs<ICreateTransactionProductViewModel> e)
        {
            OnPropertyChanged(nameof(TotalAmount));
        }

        private void OnTransactionProductQuantityDecrement(object? sender, EventArgs<ICreateTransactionProductViewModel> e)
        {
            OnPropertyChanged(nameof(TotalAmount));
        }

        private void OnCancel()
        {
            _onClose();
        }



    }
}
