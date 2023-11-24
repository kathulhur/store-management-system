using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLitePCL;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Transactions
{
    public class CreateTransactionProductViewModel : ObservableObject, ICreateTransactionProductViewModel
    {

        public CreateTransactionProductViewModel(IUnitOfWork unitOfWork, Transaction transaction, Product product)
        {
            _unitOfWork = unitOfWork;
            _transaction = transaction;
            _product = product;
            _transactionProduct = new TransactionProduct
            {
                TransactionId = transaction.Id,
                ProductId = product.Id,
                CostPrice = product.CostPrice,
                PriceSold = product.SellingPrice,
                ProductName = product.Name,
            };


            RemoveCommand = new RelayCommand(OnRemove);
            _incrementQuantityCommand = new RelayCommand(OnIncrementQuantity);
            _decrementQuantityCommand = new RelayCommand(OnDecrementQuantity, CanDecrementQuantity);

            Quantity += 1;
        }

        private readonly Transaction _transaction;

        private readonly IUnitOfWork _unitOfWork;
        private readonly Product _product;

        private readonly Action<CreateTransactionProductViewModel> _onRemove;

        private readonly TransactionProduct _transactionProduct;

        public string ProductBarcode => _product.Barcode;

        public string ProductName => _product.Name;

        public decimal Price => _transactionProduct.PriceSold;

        public int Quantity
        {
            get => _transactionProduct.QuantityBought;
            set
            {
                SetProperty(_transactionProduct.QuantityBought, value, _transactionProduct, (u, n) => u.QuantityBought = n);
                Subtotal = Quantity * Price;
                _decrementQuantityCommand.NotifyCanExecuteChanged();
            }
        }

        private decimal _subtotal;
        public decimal Subtotal { get => _subtotal; private set => SetProperty(ref _subtotal, value); }

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
            Quantity += 1;
            QuantityIncremented?.Invoke(this, new EventArgs<ICreateTransactionProductViewModel>(this));
        }


        public event EventHandler<EventArgs<ICreateTransactionProductViewModel>> QuantityDecremented;
        private readonly RelayCommand _decrementQuantityCommand;
        public ICommand DecrementQuantityCommand { get => _decrementQuantityCommand; }
        private void OnDecrementQuantity()
        {
            Quantity -= 1;
            QuantityDecremented?.Invoke(this, new EventArgs<ICreateTransactionProductViewModel>(this));
        }

        private bool CanDecrementQuantity()
            => Quantity > 1;


        public void OnDone()
        {
            _product.InStock -= Quantity;
            _unitOfWork.TransactionProductRepository.Insert(_transactionProduct);
        }
    }
}