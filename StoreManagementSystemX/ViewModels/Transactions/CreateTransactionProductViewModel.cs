using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Domain.Aggregates.Roots.Products.Interfaces;
using StoreManagementSystemX.Domain.Aggregates.Roots.Transactions.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Transactions.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class CreateTransactionProductViewModel : ObservableObject, ICreateTransactionProductViewModel
    {

        public CreateTransactionProductViewModel(
            Domain.Repositories.Transactions.Interfaces.ITransactionRepository transactionRepository, 
            ITransaction transaction,
            IProduct product
        )
        {
            _transactionRepository = transactionRepository;
            _product = product;
            _transaction = transaction;
            Price = product.SellingPrice;
            RemoveCommand = new RelayCommand(OnRemove);
            _incrementQuantityCommand = new RelayCommand(OnIncrementQuantity);
            _decrementQuantityCommand = new RelayCommand(OnDecrementQuantity, CanDecrementQuantity);

        }


        private readonly Domain.Repositories.Transactions.Interfaces.ITransactionRepository _transactionRepository;

        private readonly Action<CreateTransactionProductViewModel> _onRemove;

        private readonly ITransaction _transaction;

        private readonly IProduct _product;

        public IProduct Product => _product;

        public string ProductBarcode => _product.Barcode;

        public string ProductName => _product.Name;

        public decimal Price { get; }

        public int Quantity
        {
            get => _transaction.TransactionProducts.First(e => e.ProductId == _product.Id).QuantityBought;
        }

        public decimal Subtotal => _transaction.TransactionProducts.First(e => e.ProductId == _product.Id).TotalPrice;

        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> ItemRemoved;
        public ICommand RemoveCommand { get; }
        private void OnRemove()
        {
            ItemRemoved?.Invoke(this, new EventArgs<ICreateTransactionProductViewModel>(this));
        }


        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> QuantityIncremented;
        private readonly RelayCommand _incrementQuantityCommand;
        public ICommand IncrementQuantityCommand { get => _incrementQuantityCommand; }
        private void OnIncrementQuantity()
        {
            _transaction.IncrementProduct(_product);
            _decrementQuantityCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Subtotal));
            QuantityIncremented?.Invoke(this, new EventArgs<ICreateTransactionProductViewModel>(this));
        }


        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> QuantityDecremented;
        private readonly RelayCommand _decrementQuantityCommand;
        public ICommand DecrementQuantityCommand { get => _decrementQuantityCommand; }


        private void OnDecrementQuantity()
        {
            _transaction.DecrementProduct(_product);
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Subtotal));
            _decrementQuantityCommand.NotifyCanExecuteChanged();
            QuantityDecremented?.Invoke(this, new EventArgs<ICreateTransactionProductViewModel>(this));
        }

        private bool CanDecrementQuantity()
            => Quantity > 1;

    }
}